Console.WriteLine("Hello, World! Let's begin the transaction.");

var transactionStorage = new TransactionHandler();

try
{
    await transactionStorage.BeginAsync();
    Console.WriteLine("Transaction 1 started.");

    await transactionStorage.InsertAsync("key1", "value1");
    //Console.WriteLine("Inserted key1.");

    await transactionStorage.CommitAsync();
    //Console.WriteLine("Transaction 1 committed.");

    var value = await transactionStorage.GetAsync<string>("key1");
    Console.WriteLine($"Retrieved value for key1: {value}");

    await transactionStorage.BeginAsync();
    //Console.WriteLine("Transaction 2 started.");

    await transactionStorage.InsertAsync("key2", "value2");
    //Console.WriteLine("Inserted key2.");

    await transactionStorage.UpdateAsync("key1", "value1.2");
    //Console.WriteLine("Updated key1.");

    await transactionStorage.UpsertAsync("key3", "value3");
    //Console.WriteLine("Upserted key3.");

    await transactionStorage.CommitAsync();
    //Console.WriteLine("Transaction 2 committed.");

    value = await transactionStorage.GetAsync<string>("key1");
    Console.WriteLine($"Retrieved updated value for key1: {value}");

    await transactionStorage.BeginAsync();
    //Console.WriteLine("Transaction 3 started.");

    await transactionStorage.DeleteAsync("key2");
    //Console.WriteLine("Deleted key2.");

    await transactionStorage.UpdateAsync("key1", "value1.3");
    //Console.WriteLine("Upserted key1.");

    await transactionStorage.UpsertAsync("key4", "value4");
    //Console.WriteLine("Upserted key4.");

    await transactionStorage.DeleteAsync("key4");
    //Console.WriteLine("Deleted key4.");

    await transactionStorage.CommitAsync();
    //Console.WriteLine("Transaction 3 committed.");
}
catch(Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    await transactionStorage.RollbackAsync();
    //Console.WriteLine("Transaction rolled back.");
}
finally
{
    var value1 = await transactionStorage.GetAsync<string>("key1");
    Console.WriteLine($"Retrieved updated value for key1: {value1}");
    var value2 = await transactionStorage.GetAsync<string>("key2");
    Console.WriteLine($"Retrieved updated value for key2: {value2}");
}

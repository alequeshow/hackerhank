# Categorizing Financial Instruments in a Portfolio

## Context

A financial institution manages a diverse portfolio of financial instruments, including stocks, bonds, derivatives, and
other securities. Each financial instrument has a value representing its market price and a text indicating its type
(e.g., stock, bond, derivative).

They implement the following interface:

```
interface IFinancialInstrument
{
    double MarketValue { get; }
    string Type { get; }
}
```

Currently, there are three categories based on the market value:

- **Low Value**: Instruments with a market value less than $1,000,000
- **Medium Value**: Instruments with a market value between $1,000,000 and $5,000,000
- **High Value**: Instruments with a market value greater than $5,000,000

## Example

Input:

```
Instrument1 {MarketValue = 800000; Type = "Stock"}
Instrument2 {MarketValue = 1500000; Type = "Bond"}
Instrument3 {MarketValue = 6000000; Type = "Derivative"}
Instrument4 {MarketValue = 300000; Type = "Stock"}
```

Output:

```
instrumentCategories = {"Low Value", "Medium Value", "High Value", "Low Value"}
```

Your design must consider that category rules may be added, removed, or modified in the future, and the system
may become more complex.

Please provide your solution in C#, including classes, interfaces, methods, and any design patterns you would use
to solve this problem. Object-oriented programming is appreciated.

# SQL Procedure for Categorizing Financial Instruments

Write a procedural version of your solution for Question 1 using T-SQL (SQL Server).

Your procedure must write the inputs and outputs in one or more tables, following best practices for table modeling.

Include the script to create the table(s) in your answer

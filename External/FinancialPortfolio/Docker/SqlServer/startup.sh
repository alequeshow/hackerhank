#!/bin/sh
# Wait to be sure that SQL Server came up
sleep 90s

cd Scripts
#
# loop over the result of 'ls -1 *.sql'
#     'ls -1' sorts the file names based on the current locale 
#     and presents them in a single column
# Note: make sure that your password matches what is in the Dockerfile

for i in `/bin/ls -1 *.sql`; do 
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P MySecureP@ss! -d master -i $i
done
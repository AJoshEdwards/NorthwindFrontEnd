# NorthwindFrontEnd
A quick attempt at an app that acts as a front end for the Northwind sample database. 
Currently it uses C# code along with 3 SQL queries and a SQLConnection to pull information from the Northwind database, query 2 and 3 populate the dropdown lists with Company Country and Ship Name (via a for loop), and query 1 uses those inputs to generate the table and graph information.
There are some select statistics based on the selection to the right of the table, with number of rows populated, and average of order price shown.
The user is able to search via both Country and Ship Name, or just Country or just Ship Name, with the code using an if clause if the selected item is null then add % (wildcard) to the Query 1 parameter.

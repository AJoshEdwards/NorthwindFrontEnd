# NorthwindFrontEnd <br>

A quick attempt at an app and my first forray into Visual Studio, that acts as a front end for the Northwind sample database. Currently it uses C# code along with 3 SQL queries and a SQLConnection to pull information from the Northwind database.<br>

------------

There are 2 files within this project: <br>
&nbsp; &nbsp; • A jpg of the desktop app in action - to allow any readers quick access as I haven't fully published the app code.<br>
&nbsp; &nbsp; • The main code behind the desktop app.<br>

-----------

Brief:<br>
To create a User friendly way to quickly investigate a dataset and present the user with key information surrounding their selection.<br>

------------

Method:<br>
I downloaded and imported the Northwind sample database into SQL to use as an example.<br>
Using Visual Studio I added dropdown lists, tables and graphs which are populated by the code file. Primarily utilising SQL queries (initially as strings) to grab the initial dataset, using the SQLDataAdapter and SQLCommand to pull the information into a dataset and forloops to loop through the dataset depending on what the User has selected within the dropdown lists.<br>
Specifically query 2 and 3 populate the dropdown lists with Company Country and Ship Name (via a for loop), and query 1 uses those inputs to generate the table and graph information. <br>
There are some select statistics based on the selection to the right of the table, with number of rows populated, and average of order price shown.<br>
The user is able to search via both Country and Ship Name, or just Country or just Ship Name, with the code using an if clause if the selected item is null then add % (wildcard) to the Query 1 parameter.

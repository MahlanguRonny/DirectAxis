# DirectAxis
Respostiory used to save and track changes and source code the DirectAxis game project
-----------------------------------------------------------------
#Database project instruction
A) Open SQL managament studio and connect to your local server, i.e your IP address or computer name
B) Right click on the databases and select  "new database" and enter the name of the DB name, preferrably "DirectAxisGame" to be descriptive but can be any name of your choice and click ok
D) Clone or down load the ZIP folder and extract its content from the provided link
D) Locate the solution file inside DirectAxis/DirectAxis.RaceGame.FrontEnd folder, open it in visual studio
E)Right clck the project name DirectAxis.RaceGame.Database and set it as a start up project
F)Right click the  very same database project and select "publish" option
G) In the opened window/popup, click on "Edit" and click on "browse" tab nex to history
H) Click on "local" and pick your local server, it would be someething like <your-computer-name\SQLEXPRESS> or MSSQLLocalDB or <just your-computer-name>
I) On the same window/popup click the dropdown next to the "Database Name" chose the database name you created/provided in point B(line6), your database should be created and can be accesed via SQL management studio
J) In SQL management studio, right click your newly created DB and select "new query"
K) open the DBScripsts folder from the project repo and copy the content of "Insert Data" script into the opened query and execute
L) Your database part is done
--------------------------------------------------------------------------------------------------------------------------------------------

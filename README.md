# DirectAxis#

Respostiory used to save and track changes and source code the DirectAxis game project

# Database Instructions #
-----------------------------------------------------------------
#Database project instructions#

 Open SQL managament studio and connect to your local server, i.e your IP address or computer name
 
 -----------------------------------------------------------------
 A) Right click on the databases and select  "new database" and enter the name of the DB name, preferrably "DirectAxisGame" to be descriptive but can be any name of your choice    and  click ok 
 
 -----------------------------------------------------------------
B) Clone or down load the ZIP folder and extract its content from the provided link
 
 -------------------------------------------------------------
C) Locate the solution file inside DirectAxis/DirectAxis.RaceGame.FrontEnd folder, open it in visual studio

-----------------------------------------------------------------
D) Right clck the project name DirectAxis.RaceGame.Database and set it as a start up project

E) Right click the  very same database project and select "publish" option

F) In the opened window/popup, click on "Edit" and click on "browse" tab nex to history

G) Click on "local" and pick your local server, it would be someething like <your-computer-name\SQLEXPRESS> or MSSQLLocalDB or <just your-computer-name>
 
H) On the same window/popup click the dropdown next to the "Database Name" chose the database name you created/provided in point B(line6), your database should be created and can be accesed via SQL management studio

I) In SQL management studio, right click your newly created DB and select "new query"

J) open the DBScripsts folder from the project repo and copy the content of "Insert Data" script into the opened query and execute

K) Database project setup is done

--------------------------------------------------------------------------------------------------------------------------------------------

# Front End Console Instructions

A) Open the DirectAxis.RaceGame.FrontEnd poroject in visual studio
B) Set DirectAxis.RaceGame.FrontEnd project as a start up project
C) In the Direct.RaceGame.Data there is a appsettings.json file, open it and change the connection string server value to your computer name or IP address or any other server name that was chosen in the point (G) of the database instructions.
D) back to the FrontEnd project, run it and have a look at document named Application_Flow.dox document which can be located inside git hub repo

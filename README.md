# MVC_File_Manager
In this web application were implemented next functionality:
1) Showing catalogs structure according to task;
2) Possibility to copy your local catalogs structure in database;
3) Possibility to import or export catalogs structure in a certain file, which is located on your drive.

Home page has 5 buttons: "Add Catalog","Task Catalogs","Check Local Catalogs","Import Catalogs","Export Catalogs".

"Add Catalog" - this button allows you to add catalog to database in the table with catalogs from the Task. So please check it after checking the next button.

"Task Catalogs" - this one will move you to a new page which will show you the name of the main catalog and list of children, each one is a link for the similar info about it, as was requested in the Task.

"Check Local Catalogs" - will scan your drive or its part, then will copy the structure of your catalogs to the database and also will show the structure in the previous way.

"Import Catalogs" - will take information from a certain file which is located in a certain place, also it should have a specific name. Then the application will show it and add in the database.
For choosing the root path or file name you should adjust the "path" variable in the Import() method.
Please pay attention that for correct work of this button the file should be created in advance.

"Export Catalogs" will export information in a .txt file, you will be able to find it in downloads.

For launching application on your local machine you should adjust connection string in the appsettings.json file.

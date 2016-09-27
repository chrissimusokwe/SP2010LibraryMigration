# SharePoint 2010 Library Migration Tool

A tool for migrating SharePoint 2010 Document Libraries with metadata like "Modified" and "Modified By" kept intact. It will also migrate across sites. Remember to run it from the a machine with the SharePoint banaries installed.

This tool is a windows application that migrate SharePoint 2010 Document Libraries (excluding system Document Libraries) while maintaining metadata from a specified source site to a specified destination site. If the Document Library with the same name is found, it will merge the existing with the imported files, including library columns. Running this tool requires Farm Administrator privileges. This version of the application needs to be tested on development environment first, before being used on production.

# User Guide:

1.	Copy the folder to the root of C Drive of the SharePoint App or WFE Server.

2.	Ensure that its path is C:\LibraryMigration

3.	Ensure that the location contains the files below.

4.	To run the application, click on the file “LibraryMigration.exe” and the window below will open up.

5.	Populate your Source Site Absolute Url and Destination Site Absolute Url, and click “Start Migration”

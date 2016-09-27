# Sharepoint 2010 Library Migration Tool

A tool for migrating SharePoint 2010 Document Libraries with metadata like "Modified" and "Modified By" kept intact. It will also migrate across sites. Remember to run it from the a machine with the SharePoint banaries installed.

# Deployment Guide

# Assumptions and Prerequisites
The following assumptions and prerequisites have been made
1.	The user deploying the WSP has farm administrator privileges
2.	The Farm is a SharePoint Server 2010 implementation.
3.	At this point, only files of content type “Document” are cleaned up.
4.	The following document libraries have been ignored for clean-up.
•	Master Page Gallery
•	Form Templates
•	Site Assets
•	Site Collection Documents
•	Style Library
•	Site Pages
•	Customized Reports
•	Site Collection Images
•	and all hidden document libraries

# Deployed Artefacts
The following artefacts are packaged in the solution
1.	SPFileCleanUp.TimerJob feature, as a Web Application feature
2.	SPFileCleanUp.Site feature, as a Site Collection feature
3.	PropertyBagsSettings.aspx, as a custom page accessed at Site Collection Administration and stored in the 14 hive as /_layouts/SPFileCleanUp/PropertyBagsSettings.aspx

 
# Uploading and deploying the WSP file

The following steps are to be taken in order to add the WSP solution file to the Solution store and deploy to the relevant site collections.
1.	On the command line, assuming that the WSP has been copied to the root of c drive, run the command as seen in the screen dump below.
2.	After successfully adding to the Solution Store, the solution should be visible as seen below.
3.	Now click on the solution name to deploy it, and page as seen below will come up. 
4.	Note that the solution deploys globally, please click “OK” to deploy.
5.	Once deployed successfully, it should show as seen below.
6.	The uploading and deployment of the WSP is now done.
 
# Activation of features

The following Steps are to be taken, to activate the features at the site collection level that requires file clean up, and at its parent web application level.
Site Collection feature activation
1.	Open the site collection that you require to set clean up.
2.	Click Site Actions > Site Settings, under Site Collection Administration, click Site Collection Features.
3.	Scroll to locate the feature “SPFileCleanUp.Site” as seen below
4.	Click “Activate”, and once successfully activated it will show as seen below
5.	Click Site Actions > Site Settings, under Site Collection Administration, you will now have an option at the bottom called “Property Bag Custom Settings”
6.	Click “Property Bag Custom Settings” and a custom page will open up, showing site collection properties, as seen below.
7.	DO NOT delete nor modify any of the already existing property keys.
8.	Two property keys will need to be inserted and will be used by the SPFileCleanUp tool. Without these keys being inserted, the SPFileCleanUp will never run.
9.	The first property key to be inserted will be a key that indicates if the site collection is marked for clean up or not.
10.	 Using the text field Key, type in lowercase “spfilecleanup”, and the Value one as a digit “1”, as seen below, and click “Insert”
11.	A value of “0” would indicate that the site collection should be skipped for file clean up.
12.	Once the insert is successful, the property key should now be visible as seen below in the list of existing property keys.
13.	The second property key to be inserted will be a key that indicates the total count of file previous versions that should be returned in a file’s SharePoint version history.
14.	Using the text field Key, type in lowercase “keepversions”, and the Value as a digit “3”, to keep three previous versions, as seen below, and click “Insert”.  
15.	Once the insert is successful, the property key should now be visible as seen below in the list of existing property keys.
16.	The above values of the property keys “spfilecleanup”  and “keepversions” can be edited at any later stage, depending on the site collection requirements, by the site collection administrator.
17.	The above steps of activating and configuring at Site Collection would have to be repeated for every site collection that requires clean up.
 
# Web Application feature Activation

1.	Open Central Administration site
2.	Under Application Management section, click “Manage Web Applications”
3.	Highlight the Web Application that you wish to schedule file clean up on, as seen below.
4.	On the ribbon, under Manage section, click “Manage features”
5.	Scroll to locate the feature “SPFileCleanUp.TimerJob” as seen below.
6.	Click “Activate” to activate the web application feature, and once successfully activated, it should show as seen below.
7.	While on Central Administration, navigate to “Monitoring” page.  
8.	Under Timer Jobs sections, click “Review job definitions”, and scroll to locate the job definition called “Clean Up Schedule Timer Job”. 
9.	Click the job definition “Clean Up Schedule Timer Job” to edit it and the page shown below will come up with default settings.  
10.	Change the scheduled time to run the job according to your requirement, in our case, we schedule it to run daily, starting at 6PM but no later than 7PM, as seen below  
11.	The above steps for Web Application feature activation will have to be repeated for every Web Application that requires file clean up.

# Event Logging

The SPFileCleanUp tool writes to the windows event logs the following activities:
1.	The total count of versions per file that have been cleaned up.
2.	The file name cleaned up
3.	The time the file was cleaned up
4.	Any errors it may come across during the clean-up process.
 

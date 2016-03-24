INTRODUCTION
------------

This is a sample application that demonstrates how data can be imported into AX7 instance from 
an "external" data source. The sample demonstrates this by assuming an on-premise system that
has 4 source locations through a data file is taken through (as shown below).



										 '---->  << Success >>
										 '
<< Input >>  -->  << In-process >> ---'
										 '
										 '
										 '---->  << Failure >>


The application works by have the user submit data files that are to be imported, to the "input" location.
This data file is then moved to "inprocess" location at which point the data is submitted to
AX7 using the "Enqueue" API. The application then proceeds to check the status of the submitted data files 
using the "JobStatus" API, before moving the data files in the client side to either the "Failure" or the "Success" 
location. This application only moves files to "Success" location if all the records in the data files are 
processed without any errors. Any transmit or business errors results in the data file being moved to the "Failure" location.
A log file is also saved in the "Success"/"Failure" location that indicates a detailed status of the processing detail
or the errors encountered.


PRE-REQUISITES
---------------

1. Ensure you can download the project locally and build it successfully.

NOTE: There are nuget package references (see packages.config). To enable automatic 
package restore while building this sample, enable all options in your VS IDE
 - Tools > Options > Nuget Package Manager > General tab

2. CLIENT SIDE REQUIREMENTS

a. 4 locations (the sample uses shared folders as source locations) one each for - input, inprocess, 
failure and success 

b. This application can be extended to support any "datasource" for any or all of the locations.


3. SERVER REQUIREMENT

a. A valid, provisioned AX7 instance and a few provisioned users. The 
"AX7 endpoint", "AX7 user name" and "AX7 user password" require this.

b. A native client app that has permissions setup for Dynamics Connector APIs.

4. DATA TRANSFER REQUIREMENTS

a. A valid recurring data job.
NOTE: A recurring data job has a "ID" that is also knows as the "ActivityId". This ID must be 
specified against the "ActivityId" property in the client app

NOTE: Refer documentation on how to create a data project and an associated recurring data job
here -> https://ax.help.dynamics.com/en/wiki/recurring-integrations/


RUNNING THE APPLICATION
------------------------

1. Once all pre-reqs are met, edit the "App.config" file and update the corresponding key values 
with the actual environment values that you have setup.

2. Save the app.config and launch the application

3. Validate that all the values are correct.

4. Click on the "Execution" tab in the application

5. Click "Start". Now your application is ready to process the data files...

NOTE: As files are processed, they are put in either the failure or success location 
based on their processing status. The actual data file is moved to the location. In
addition, a log file containing either the HttpResponse details or the actual JobStatus API
response is also saved with the following format <<file name>>_log.json or << file name>>_log.txt

 
THINGS TO NOTE
---------------

1. The CTP8 platform does not support the JobStatus API and hence this sample will not work as-is
against such AX7 instances. You may have to update the sample to short-circuit the JobStatus
related portions in the DefaultDataFlowNetwork.cs class to use it in such scenarios

2. To extend using different datasources as any of the supported locations (input, inprocess, success or failure),
the following must be done:

a. Implement - IDataSource<ClientDataMessage>. See SharedFolderDataSource.cs for sample

b. Update the DataSourcefactory to return the correct datasource based on the location or message URI scheme or based
on other location properties.

c. If the custom datasource is to be used as an input location for files, the the FileProcessor.cs must be updated to
set up polling the input location (see initDatasourceWatcher method), setup a message pump like the 
"sharedFolderFileCreatedHandler" method. Notice that the DataSources will implement the "FindAll" method that can
be used to enumerate and post the ClientDataMessages to the data processing class.

3. The file processing is modeled as a series of stages through which the data processing proceeds.
This "data flow network" logic is contained in the "DefaultDataFlowNetwork" class. 
You can implement your own strategy that does something similar or automates a different document state flow.

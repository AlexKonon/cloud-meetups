# How-to guide. HTTP triggered Azure function creation.

### Prerequisites
* Create Azure account
* Install VS2019 within Azure development workload - https://visualstudio.microsoft.com/vs/visual-studio-workloads/.
For the VS2017 use the latest Azure Functions tools https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs#check-your-tools-version.


### How to create Azure function sample project
* Open VS > File > New > Project.
* In the dialog search for ‘Azure Functions’ template and select Next.
* Enter a name for your project and press Select.
* Then use the following options in the dialog:
- Functions runtime: Azure functions v2 (.Net Core)
- Trigger: Http trigger
- Storage account: None
- Authorisation level: Anonymous

## Note: 
The values above were chosen to simplify sample project creation process. There are a bunch of trigger types, authorisation levels. Function runtime supports .Net framework as well. Storage account must be created and specified for any other trigger type except Http trigger.


### How to run the function locally and debug
* Run the function in debug configuration.
* Then you’ll observe CLI window and output log. Find out the function http uri and copy it.
* Use Postman to generate [GET] or [POST] request with name param and unsure that you receive 200 status code.

## Note: 
Once function is executed in debug mode you can specify breakpoints and debug it in classic way.

### How to publish function to Azure
* Right-click the project and in the appeared menu select Publish.
* Then use the following options in the dialog:
- Choose Azure function consumption plan.
- Check ‘Create new’ or ‘Select Existing’ depending on what you need, create totally new azure function or override existing one.
- Check ‘Run from package file’.

* Click Publish.
* Then use the following options in the dialog:
- Create new Resource Group or choose existing if created before.
- Choose Consumption for Hosting plan and specify the nearest Location in the region.
- Create new Azure Storage or choose existing if created before.

* Click Create to create resources and deploy your function to Azure.
* After the deployment is finished you’ll observe function base address and can try to execute it.

### Useful links:
For more information, various guides, best practices and explanations regarding Azure Function please refer the documentation:
https://docs.microsoft.com/en-us/azure/azure-functions/

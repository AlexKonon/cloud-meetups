# How-to guide. HTTP triggered AWS Lambda function creation.


### Prerequisites
* Create AWS account
* Install VS 2019 within AWS Toolkit - https://aws.amazon.com/visualstudio/ or add as an extension directly from VS.

### How to create AWS Lambda sample project
* Go to Identity and Access Management (IAM) service > Groups > Create New Group.
- Specify group name
- Assign AwsLambdaFullAccess policy.
* Go to Identity and Access Management (IAM) service > Users section > Add.
- Enter user name. 
- Check Programmatic access.
- Uncheck AWS management console access.
- Add user to recently created group.
- Skip tags creation.
- Download csv file with settings to use it further.
* Go to Identity and Access Management (IAM) service > Roles section > Create role.
- Click on Lambda from the list.
- Search and check for AwsLambdaBasicExecutionRole.
- Skip tags creation.
- Specify role name and click Create.
* Open VS > AWS Explorer (could be found under menu item View > AWS Explorer).
- Add New Account Profile:
- Specify profile name.
- Import settings from recently downloaded csv file.
* Open VS > File > New > Project.
* In the dialog search for AWS Lambda template, choose AWS Lambda Project for .Net Core and select Next.
* Enter a name for your project and press Create.
* Chose Empty function from the further dialog.
* Add a Amazon.Lambda.ApiGatewayEvents nuget package
* Change string input argument to ApiGatewayProxyRequest
* As a return type choose ApiGatewayProxyResponse.

### How to run the function locally and debug
* Run the function in debug configuration within Mock Lambda Test Tool.
* Then you’ll observe web page with areas to configure and specify input parameters.
* Choose profile, settings and specify input json. Click Execute Function.

## Note: 
Once function is executed in debug mode you can specify breakpoints and debug it in classic way.

### How to publish function to AWS
* Right-click the project and in the appeared menu select Publish to AWS Lambda.
* Then use the following options in the dialog:
- Choose account profile.
- Specify appropriate region.
- Enter function name
* Click Next.
* Then use the following options in the dialog:
- Choose recently created role name
- Set memory and timeout numbers.
* Click Upload to deploy your function to AWS.
* After the deployment is finished you can go to AWS Console > Lambda and observe function new function under the correct region.

### How to make HTTP trigger for AWS Lambda
* AWS Console > Lambda > Your created lambda > Add trigger from designer view.
* Select API Gateway and the following options further:
* API: Create a new API
* Security: Open
* Press create.
* Navigate to triggers section from designer view and copy the link for trigger.
* Use Postman to generate a request and unsure that you receive 200 status code.

### Useful links:
For more information, various guides, best practices and explanations regarding AWS Lambda please refer the documentation:
- https://docs.aws.amazon.com/lambda/latest/dg/welcome.html

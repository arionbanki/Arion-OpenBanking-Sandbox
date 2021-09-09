![Logo](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/01%20-%20arionlogoblue.png?raw=true)
# Lausnamót 2021 - Arion's Open Banking platform


## Documentation


  ### Getting Started

  TPP - Quick Onboarding registration
  
  Login/Register to the Arion WebPortal

Go to https://developers.arionbanki.is/ and click "Get Started"


![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/02%20-%20Getting%20Started.png?raw=true)




Select Login Provider of your choice, Github or Azure

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/03%20-%20Choose%20Provider.png?raw=true)
  
After you've successfully logged in, 
Click "Add Application"

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/04%20-%20Create%20your%20first%20application.png?raw=true)

Enter the name of your Application

Enter a public description of your Application (optional)

Click "Create Application"

![App Screneshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/05%20-%20Application%20Details%20Create.png?raw=true)

After that you'll be enrolled in our PSD Sandbox, and you'll see the app details, e.g. ApiKey, ClienId and your ClientSecret.

You will use this data in your code to connect to Arion's OpenBanking services.
( CliendId and ClienSecret is not currently needed for demo purposes now )

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/06%20-%20My%20Demo%20App.png?raw=true)


Next head into the Users menu and create your Users

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/07%20-%20Manage%20Users.png?raw=true)

After you've hit "Add User" - then select your newly created user in the grid to generate data for him/her

To create data for that user, hit "Create Account":

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/08%20-%20Create%20Accounts.png?raw=true)

Your newly created test data will be displayed:

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/09%20-%20Accounts%20Data.png?raw=true)

Next head into your Application details page and generate a token. This token will be used to connect to our OpenBanking Api services:

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/10%20-%20Generate%20Token%20Button.png?raw=true)

Select your application and the person you want to generate a token for. If you've created multiple users, you will get multiple users from this dropdown. If not, you'll only see one user:

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/11%20-%20Create%20Token.png?raw=true)

Next, click "Create Token" tok get your OpenId token to use in your client for our OpenBanking services
![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/12%20-%20Token%20Generation.png?raw=true)

Your token will be created and you can copy the value to use in your application, and set it as bearer in the http header of your application:

( please note this token will only be displayed once in this page - you can however create a new token anytime you like)
![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/13%20-%20Token%20Created.png?raw=true)

Next you can download our sample client code from https://github.com/arionbanki/Arion-OpenBanking-Sandbox/tree/main/IsIT.OpenBanking.Sandbox.DeveloperConsole
and put the data you've created in the previous steps into the Program.cs client to connect:
![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/14%20-%20DevConsole%20Demo%20Code.png?raw=true)

!!!!!!TODO bæta við hvernig maður uploadar skrám í UI'inu ...!!!!!!

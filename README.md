![Logo](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/01%20-%20arionlogoblue.png?raw=true)
# Arion's Open Banking platform


## Documentation


  ### Getting Started

  TPP - Quick Onboarding registration
  
  Login/Register to the Arion WebPortal

Go to https://developer.arionbanki.is/ and click "Get Started"


![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/02%20-%20Getting%20Started.png)

After you've successfully logged in, fill in your information:

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/03%20-%20Lets%20Get%20To%20Know%20You.png?raw=true)

Now you need to go to registered developer's email and confirm it

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/04%20-%20Confirm%20Email.png?raw=true)

After you've confirmed your email you will get a notification like this and you'll be redirected to the main page to create your first application

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/05%20-%20Email%20Confirmed.png?raw=true)

Click "Add Application"

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/06%20-%20Create%20your%20first%20application.png?raw=true)

Enter the name of your Application

Enter a public description of your Application

Enter the redirectUri of your Application

Then hit the "Next Step" button

![App Screneshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/07%20-%20Application%20Details%20Create.png?raw=true)

After that you'll have to select your product, in this case the Arion Sandbox.

Click "Next Step"

![App Screneshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/08%20-%20Application%20Details%20Create.png?raw=true)

Next fill out your company details

*please note:
eIDAS identifer and the QWAC Certificate fields are not need untill you hit the GoLive button*

Click "Next Step"

![App Screneshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/09%20-%20Application%20Details%20Create.png?raw=true)

Attention! On the next screen you will see your client secret. You will only see this once and never again so please make note of it and store is somewhere safe.

*( if you loose this secret in the future, there is an option to re-generate the client )*

![App Screneshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/11%20-%20Application%20Created.png?raw=true)

Next please confirm the final registration by confirming the email sent do the registered company ( the TPP ) you entered previously

![App Screneshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/12%20-%20Confirm%20The%20Email.png?raw=true)

After that you'll be enrolled in our PSD Sandbox, and you'll see the app details, e.g. ApiKey, ClienId and the scopes your App has access to.

You will use this data in your code to connect to Arion's OpenBanking services, along side with the Client Secret you noted and stored earlier.

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/13%20-%20My%20App%20Details.png?raw=true)

Next head into the Data menu and create your Users

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/14%20-%20Manage%20Users.png?raw=true)

Hit "Add User" - then select your newly created user in the grid to generate data for him/her

To create data for that user, hit "Create Account":

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/15%20-%20Create%20Accounts.png?raw=true)

Your newly created test data will be displayed:

![App Screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/16%20-%20Accounts%20Data.png?raw=true)

Next you need to generate a token. For the Sandbox, it's possible to do that by hitting the "..." for your application and selecting "Create Token"

This token will be used to connect to our OpenBanking Api services:

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/17%20-%20Generate%20Token%20Button.png?raw=true)

Select your App and the person you want to generate a token for. If you've created multiple users, you will get multiple users from this dropdown. If not, you'll only see one user.

Click "Create Token" tok get your OpenId token to use in your client for our OpenBanking services
![App screenshot](https://raw.githubusercontent.com/arionbanki/Arion-OpenBanking-Sandbox/main/doc-images/18%20-%20Token%20Generation.png)

Your token will be created and you can copy the value to use in your application, and set it as bearer in the http header of your application:

( please note this token will only be displayed once in this page - you can however create a new token anytime you like)
![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/19%20-%20Token%20Created.png?raw=true)

Next you can download our sample client code from https://github.com/arionbanki/Arion-OpenBanking-Sandbox/tree/main/IsIT.OpenBanking.Sandbox.DeveloperConsole

and put the data you've created in the previous steps into the Program.cs client to connect:

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/20%20-%20DevConsole%20Demo%20Code.png?raw=true)

You're all set! ;-)

## Appendix

### Create More Test Data
If you want to upload more example data for the user created inside the webportal, you can do that.
Under users, hit the upload button and you can download a template with example data, so more data can be added to your test user.

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/21%20-%20Download%20A%20Template.png?raw=true)

When you hit the "Download a example" you'll get a CSV file which you can modify as you want to create new records:

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/22%20-%20Excel%20Example.png?raw=true)


After you've created your data, you can hit the "Choose file" to upload it for the selected user:
![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/23%20-%20Upload.png?raw=true)



### Calling the OpenBanking Api
Here is a detailed documentation for our it:
https://arion-iobws-3.redoc.ly

Please note that you need the "openbanking.read" and "openbanking.readwrite" scopes to be able to access our OpenBanking Api
- it is already supplied in the token you get from our OpenBanking WebPortal, as you can see by inspecting the token ( e.g. by pasting it into www.jwt.io ):

An example token:

```javascript
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiLDnsOzcmV5IFLDs3Mgw57DjSDDk2xhZnNkw7N0dGlyIiwiaHR0cDovL3NjaGVtYXMuYXJpb25iYW5raS5pcy93cy8yMDIwLzA1L3NhbmRib3gvY2xhaW1zL2FwaWtleSI6ImJiYzZlOGYyZjcyOTRkMDJhMjkzN2MzYjNhODY5YjBiIiwiaHR0cDovL3NjaGVtYXMuYXJpb25iYW5raS5pcy93cy8yMDIwLzA1L2NsYWltcy9rZW5uaXRhbGEiOiIxOTEyNjMxNDY5IiwiaHR0cDovL3NjaGVtYXMuYXJpb25iYW5raS5pcy93cy8yMDIwLzA1L2NsYWltcy9hcHBpZCI6IlRoZUNsaWVudCIsInNjcCI6InByb2ZpbGUsIG9wZW5iYW5raW5nLnJlYWQsIG9wZW5iYW5raW5nLnJlYWR3cml0ZSIsIm5iZiI6MTYzMTE3OTgwNiwiZXhwIjoxNjMxMjM5ODA2LCJpc3MiOiJBcmlvblNhbmRib3ggRGV2IElzc3VlciIsImF1ZCI6InVybjppb2J3cyJ9.EaX-MSr885v5ZW8834Ozx9orPJbEUHh3zt6__9ln0HM
```

the values for the token look like this:

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/25%20-%20Example%20Token%20Scopes.png?raw=true)

#### Creating a consent
In the PSD2 directive consents are prerequisite to 
* Access Account Balances and Transactions,
* and Initiate And Authorize Payment

So to be able to use our OpenBanking Services, you need to create a consent

Later when calling the Accounts or Payments endpoints you use the id which you get back when creating a consent

These are the parameters required to create a valid consent, which you then need to confirm before calling our OpenBanking Apis:
1. X-Request-ID header - Valid UUID
2. PSU-ID header - Valid kennitala of the natural or legal person making use of a payment service - the user which you get from our Openbanking WebPortal 
3. Ocp-Apim-Subscription-Key header - API key which you get from our Openbanking WebPortal 
4. PUS-IP-Address header -  Valid IP Address
5. Authorization header - Valid Access token which you get from our Openbanking WebPortal 

There's an example in our demo test-client on how to do that in .Net core:
( below the image is an example of how the actual call looks like, for example if you're using another programming language than .Net )
![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/26%20-%20Create%20Consent%20In%20Net.png?raw=true)


The http call looks like this - example for creating a consent:

```javascript
curl -X POST "/v1/consents" -H "accept: text/plain" -H "X-Request-ID: <random-UUID>" -H "PSU-IP-Address: <IP-Address>" -H "PSU-ID: <PSU-ID>" -H "Ocp-Apim-Subscription-Key: <API-Key>" -H "Authorization: Bearer <AccessToken>" -H "Content-Type: application/json-patch+json" -d "{\"access\":{\"accounts\":[{\"iban\":\"IS970352264747671912631469\",\"bban\":\"035226474767\",\"pan\":null,\"maskedPan\":\"492500******1234\",\"msisdn\":null,\"currency\":\"ISK\"},{\"iban\":\"IS340395263302831912631469\",\"bban\":\"039526330283\",\"pan\":null,\"maskedPan\":null,\"msisdn\":null,\"currency\":\"ISK\"}],\"balances\":null,\"transactions\":null,\"availableAccounts\":null,\"availableAccountsWithBalance\":null,\"allPsd2\":null},\"recurringIndicator\":true,\"validUntil\":\"2021-09-02T14:35:47.0470702+00:00\",\"frequencyPerDay\":4,\"combinedServiceIndicator\":false}"
```

and here's an example of the return type ( the actual consent ):
```javascript
{
  "consentStatus": "received",
  "consentId": "151",
  "scaMethods": null,
  "chosenScaMethod": null,
  "challengeData": null,
  "_links": {
    "scaRedirect": {
      "href": "https://localhost:44303/psd2/scas/authorize?clientid=TheClient&response_type=code&scope=openid+CO%3A151&redirect_uri="
    },
    "self": {
      "href": "/v1/consents/151"
    },
    "status": {
      "href": "/v1/consents/151/status"
    },
    "scaStatus": {
      "href": "/v1/consents/151/authorisations/c6d7b64f-bbea-4d8e-9866-73b7c714db31"
    }
  },
  "psuMessage": null
}
```

To confirm a consent, which is needed before calling our OpenBanking Apis you open up the scaRedirect link which you get back from the newly created consent,
where you can confirm the consent:

For example, in the example above it was:
```javascript
scaRedirect": {
      "href": "https://localhost:44303/psd2/scas/authorize?clientid=TheClient&response_type=code&scope=openid+CO%3A151&redirect_uri=" }
```

and if we open up that path we get this page, where we can authorize the consent:

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/27%20-%20Consents.png?raw=true)

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/28%20-%20Consent%20agreed.png?raw=true)

All done! You're ready to call the actual OpenBanking Apis now ;-)

#### How to Access Account Balances and Transactions

Prerequisite: to be able to call these services, you need to have confirmed consent available, see the "Creating a consent" above for more info.

To get account data, you use the AccountsInformationServiceApi, which is located here:
https://arion-iobws-3.redoc.ly#tag/AccountInformationServiceApi

These are the parameters required to get Account List by calling our OpenBanking Apis:
1. X-Request-ID header - Valid UUID
2. consentID header - Valid Consent Id, Created here which you get by calling the consent service in the above step
3. Ocp-Apim-Subscription-Key header - API key which you get from our Openbanking WebPortal
4. Authorization header - Valid Access token which you get from our Openbanking WebPortal
5. withBalance parameter - true/false


There's an example in our demo test-client on how to do that in .Net core:
( below the image is an example of how the actual call looks like, for example if you're using another programming language than .Net )

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/29%20-%20AccountsList.png?raw=true)

The http call looks like this - example for getting accounts:

```javascript
curl -X GET "/v1/accounts?withBalance=true" -H "accept: text/plain" -H "X-Request-ID: <random-UUID>" -H "consentID: <Consent-ID>" -H "Ocp-Apim-Subscription-Key: <API-Key>" -H "Authorization: Bearer <AccessToken>"
```

#### How to Initiate and Authorize a Payment

Prerequisite: to be able to call these services, you need to have confirmed consent available, see the "Creating a consent" above for more info.

To get payment data, you use the PaymentsServiceApi, which is located here: https://arion-iobws-3.redoc.ly#tag/PaymentInitiationServiceApi

These are the parameters required to do a payment by calling our OpenBanking Apis:
1. Go to /v1/payments/sepa-credit-transfers. 
2. X-Request-ID header - Valid UUID
3. Consent-ID header - Valid Consent Id, Created here which you get by calling the consent service in the above step
4. Ocp-Apim-Subscription-Key header - API key which you get from our Openbanking WebPortal
5. PSU-IP-Address header -  Valid IP Address
6. Authorization header - Valid Access token which you get from our Openbanking WebPortal
7. payment-service parameter: payments
8. payment-product parameter: sepa-credit-transfers


There's an example in our demo test-client on how to do that in .Net core:
( below the image is an example of how the actual call looks like, for example if you're using another programming language than .Net )

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/30%20-%20Payments%20Initiate.png?raw=true)

The http call looks like this - example for creating a payment:

```javascript
curl -X POST "/v1/payments/sepa-credit-transfers" -H "accept: text/plain" -H "X-Request-ID: <random-UUID>" -H "PSU-IP-Address: <IP-Address>" -H "Ocp-Apim-Subscription-Key: <API-Key>" -H "X-Request-Context-User: 6093608" -H "Authorization: Bearer <Access-Token>" -H "Content-Type: application/json-patch+json" -d "{\"EndToEndIdentification\":\"beb9d5da517040fa99d619574f6ccb72\",\"DebtorAccount\":{\"Iban\":\"IS970352264747671912631469\",\"Bban\":null},\"DebtorId\":\"1912631469\",\"UltimateDebtor\":null,\"UltimateDebtorId\":null,\"InstructedAmount\":{\"Currency\":\"EUR\",\"amount\":\"1\"},\"CreditorAccount\":{\"Iban\":\"IS340395263302831912631469\",\"Bban\":null},\"CreditorAgent\":\"ESJAISRE\",\"CreditorAgentName\":null,\"CreditorName\":\"Creditor Name\",\"UltimateCreditor\":null,\"UltimateCreditorId\":null,\"IcelandicPurpose\":null,\"RemittanceInformationUnstructured\":\"my description\",\"RequestedExecutionDate\":null}""
```

#### How to Request Information about Available Funds

Prerequisite: to be able to call these services, you need to have confirmed consent available, see the "Creating a consent" above for more info.

To get information about Available Funds, you use the PayemntsServiceApi, which is located here: https://arion-iobws-3.redoc.ly#tag/ConfirmationOfFundsServiceApi

These are the parameters required to get confirmation of funds by calling our OpenBanking Apis:
1. Go to /v1/accounts/{account-id}/balances. 
2. accountId - Valid account Id
3. X-Request-ID header - Valid UUID
4. Consent-ID header - Valid Consent Id, Created here which you get by calling the consent service in the above step
5. Ocp-Apim-Subscription-Key header - API key created here
5. Authorization header - Valid Access token which you get from our Openbanking WebPortal

There's an example in our demo test-client on how to do that in .Net core:
( below the image is an example of how the actual call looks like, for example if you're using another programming language than .Net )

![App screenshot](https://github.com/arionbanki/Arion-OpenBanking-Sandbox/blob/main/doc-images/31%20-%20Confirmation%20Code%20Example.png?raw=true)

The http call looks like this - example for getting confirmation of funds:

```javascript
curl -X GET "/api/v1/accounts/<account-id>/balances" -H "accept: text/plain" -H "X-Request-ID: <random-UUID>" -H "Consent-ID: <Consent-ID>" -H "Ocp-Apim-Subscription-Key: <API-Key>"  -H "Authorization: <Access-Token>"

```

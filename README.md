# Community Programs Unit

The portal application is two pieces.

**Dotnet Core back end:** specifics about versions are found in the `cpu-app/cpu-app.csproj` file.
**Angular front end:** specifics are found in the `cpu-app/ClientApp/package.json` file.

## Overview

### Angular front-end

The front end is built for use by a social service agency or general public. All government facing parts of the interaction with the data are handled in Dynamics. Therefore this front-end acts as a data collection area outside of Dynamics. All functionality that gets data from Dynamics should be located behind a protected siteminder route.

The front end contains forms which are built from subcomponents. Each form has its own page component, service, transmogrifier object, and convert-to-dynamics function. The template syntax never interacts with Dynamics data or values directly and relies on a conversion layer to convert the data into viewmodels that bind to forms and components. On a submission, a function pulls relevant information into something that can be sent back to the back end and easily cleaned for Dynamics.

Since a user can save and continue, form validation is done at a field level and the user can save the forms in an invalid state. Clicking "submit" should determine if all of the information makes sense together and that mandatory parts are included.

### Dotnet Core back-end

The back end runs in openshift and takes HTTP requests from the front end. It cleans those HTTP requests into json that can be used with Dynamics. It then gets appropriate credentials and negotiates a connection with Dynamics then performs the interaction and returns the results from Dynamics back to the front end where the user can interact.

# Developer setup

## Prerequisites

1. Cisco AnyConnect and vpn credentials. All interactions between the back-end and Dynamics happen within the government network. If you are developing the application locally, your machine needs access to Dynamics the same way as the server hosting it. That the data source is obviously not going to be internet facing.
2. [Microsoft Dotnet Core](https://dotnet.microsoft.com/download/dotnet-core) - Download the version that matches the one listed in the `cpu-app/cpu-app.csproj` file and install it.
3. [Node.js](https://nodejs.org) - Usually the latest version is suitable. OpenShift's Dotnet Core image is in control of which version is used when deployed. That means an openshift build may have unexpected problems on a build. The safest is to use whatever version of Node.js that Microsoft has chosen to support in their dotnet core image.

## Running the Project for the first time

### User secrets

[Setup secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows#secret-manager)

### Secrets Template

```JSON
{
  "BASE_PATH": "/coastcontracts",

  "CONFIGURATION_OUTAGEINFORMATION_MESSAGE": "",
  "CONFIGURATION_OUTAGEINFORMATION_STARTDATE": "",
  "CONFIGURATION_OUTAGEINFORMATION_ENDDATE": "",

  "Dynamics": {
    "AuthenticationType": "OnPremise",
    "ADFS": {
      "DynamicsApiEndpointUrl": "http://dev-coast-dataverse-proxy.silver.devops.bcgov/api/data/v9.0/",
      "OAuth2TokenEndpoint": "https://ststest.gov.bc.ca/adfs/oauth2/token",
      "ClientId": "<onpremise_client_id>",
      "ClientSecret": "<onpremise_client_secret>",
      "ServiceAccountName": "<onpremise_service_account_username>",
      "ServiceAccountPassword": "<onpremise_service_account_password>",
      "ResourceName": "https://cscp-vs.dev.jag.gov.bc.ca/api/data/v9.0/"
    },
    "EntraId": {
      "DynamicsApiEndpointUrl": "https://cscp-dev.api.crm3.dynamics.com/api/data/v9.2/",,
      "TenantId": "<cloud_tenant_id>",
      "ClientId": "<cloud_client_id>",
      "ClientSecret": "<cloud_client_secret>",
      "ResourceName": "https://cscp-dev.api.crm3.dynamics.com"
    }
  },

  "KEYCLOAK_CLIENT_ID": "coast",
  "KEYCLOAK_CLIENT_SECRET": "<keycloak_secret>",
  "KEYCLOAK_GRANT_TYPE": "client_credentials",
  "KEYCLOAK_URL": "https://dev.common-sso.justice.gov.bc.ca/auth/realms/Services/protocol/openid-connect/token",

  "JWT_TOKEN_KEY": "<jwt_token_key>",
  "JWT_VALID_ISSUER": "https://cscp-vs.dev.jag.gov.bc.ca/",
  "JWT_DISABLE_ISSUER_VALIDATION": "true",

  "JAG_CLIENT_ID": "<jag_client_id>",
  "JAG_CORRELATION_ID": "<jag_correlation_id>",
  "JAG_DOCUMENT_MERGE_URL": "<jag_document_merge_url>",

  "SITEMINDER_LOGOUT_URL": "https://logontest7.gov.bc.ca/clp-cgi/logoff.cgi"
}
```

```bash
# Windows secrets location:
C:\Users\<user account short name>\AppData\Roaming\Microsoft\UserSecrets\<user_secrets_id>\secrets.json

# Mac/Linux Secrets location:
~/.microsoft/usersecrets/<user_secrets_id>/secrets.json
```

### Run the Angular app

1. Open a shell like `bash` or `zsh`.
2. Change directories into the `cpu-app/ClientApp` folder in the project.
3. Run the command `npm install` to build the `node_modules` folder from the dependencies listed in `package.json`. _(You may or may not need to `npm install -g @angular/cli` to install the Angular CLI tools.)_
4. Run `npm start` to start the Angular app.

### Run the Dotnet API

1. Set Dotnet Core developer secrets. These will need to be attained from a Dynamics admin, exported from OpenShift, or copied from another developer.
2. Open a shell like `bash` or `zsh`.
3. Change directories into the `cpu-app` folder in the project.
4. Run the code using `dotnet run` or `dotnet watch run`. Adding `watch` will automatically reload the project when a `.cs` or `.ts` files changes on disk. This is very handy for development.

## Making Code Changes

Any plaintext editor or IDE can be used to make file changes on disk. The important part is things like syntax highlighting and other useful tools that help you identify mistakes early.

VS Code is a good editor and comes with a built-in terminal that we can use for running the project. Helpful plugins:

1. Add the plugin "Angular Language Service" by **Angular** for front-end syntax help.
2. Add "Manage User Secrets" by **Reptarsrage** for dotnet core secrets management.
3. Add "C#" by **Microsoft** for dotnet core syntax highlighting.

```

```

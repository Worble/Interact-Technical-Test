# Interact Techinical Test

# Project Structure

`/InteractTechnicalTest` contains the startup web project

* `/InteractTechnicalTest/app_root` contains the front-end elements that need to be built seperately

`/InteractTechnicalTestDomain` contains all domain elements, such as POCOs

`/InteractTechinicalTestInfrastructure` contains all infrastructure code, such as database contexts and repositories

`/InteractTechnicalTestInfrastructureTests` contains tests for infrastructure code

# Building From Source

* Navigate to `/InteractTechnicalTest` and run `dotnet build` to build the web project. This should also copy the two test databases `products_litedb.db` and `products_sqlite.db` from the root to the bin folder. You can switch between using these two by commenting and uncommenting the dependency resolution for Unity in `\InteractTechnicalTest\App_Start\UnityConfig.cs`.

* Navigate to `/InteractTechnicalTest/app_root`. Run `yarn` or `npm i` to install dependencies.
    * Run `yarn dev` or `npm run dev` to create an unoptimized build.
    * Run `yarn watch` or `npm run watch` to create  an unoptimized build and watch for changes.
    * Run `yarn prod` or `npm run prod` to create an optmized production build.

The project should now be ready to be run.

# Running Tests

* Navigate to `/InteractTechnicalTestInfrastructureTests`. Run `dotnet test`.
* Navigate to `/InteractTechnicalTest/app_root`. Run `yarn` or `npm i` to install dependencies and then run `yarn test` or `npm run test`

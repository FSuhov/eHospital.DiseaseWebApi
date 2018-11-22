# eHospital.DiseaseWebApi
This is a part of larger project of web-interface and digital data storage for hospital.
eHospital.DiseaseWebApi implements back-end for Diseases part.

It contains controllers, business logic and tests for handling Http requests:
- GET /api/disease/ == Retrieves all diseases from DB sorted alphabetically by name
- GET /api/disease/categoryid=2 == Retrieves all diseases belonging to choosen Category
- GET /api/disease/2 == Loooks for and retrieve specific disease By ID
- POST /api/disease/[FROM BODY] Disease == Adds new Disease to DB
- DELETE /api/disease/2 == Sets specific Disease to IsDeleted

- GET /api/diseasecategory/ == Retrieves all Catgories of Diseases from DB
- GET /api/diseasecategory/2 == Retreives specific Category by ID
- POST /api/diseasecategory/[FROM BODY] DiseaseCategory == Adds new Category

- GET /api/patientdisease/names/2 == Retrives all Diseases related to specific Patient in simple view
- GET /api/patientdisease/2 == Retrives all Diseases related to specific Patient in detailed view
- GET /api/patientdisease/patientdisease=2 == Retrieves most detailed view of specific PatientDisease
- POST /api/patientdisease/[FROM BODY patientDisease] == Adds new PatientDisease entry
- PUT /api/patientdisease/id?={}id + [FROM BODY patientDiseaseDetails] == Updates existing PatientDisease entry
- DELETE /api/patientdisease/2 == Sets IsDeleted to TRUE for specific PatientDisease
=========================================================================
User stories to be matched:
1. Disease
1.1. As a doctor/nurse/admin I want to get list of all diseases
1.2. As a doctor/nurse/admin I want to see the list of all diseases of chosen category
1.3. As a doctor/nurse/admin I want to see extended information about disease: Name, Category name,
Description.
1.4. As an admin I want to add new disease
1.5. As an admin I want to delete disease

2. Disease Categories
2.1. As a doctor/nurse/admin I want to see the list of all disease categories
2.2. As an admin I want to add new disease categories

3. Patient Disease
3.1. As a doctor/nurse/admin I want to see the list of names of patient`s diseases
3.2. As a doctor/nurse/admin I want to see a table of patient`s diseases with columns: Disease name,
Disease category, Start date, Status (current or finished), Doctor (FirstName + LastName)
3.3. As a doctor/nurse/admin I want to get extended description of patient`s disease: Disease name,
Category, Description, Start date, End Date, Doctor`s Notes, Doctor (FirstName + LastName)
Adding Patient Disease
3.4. As a doctor/admin I want to add the disease to patient
Deleting patient`s disease
3.5. As an admin I want to delete disease from patient`s diseases
Updating patient`s disease
3.6. As a doctor/admin I want to update End Date, Doctor`s notes, Doctor`s Name in extended description
of patient`s disease
=========================================================================

The project implements the following features:
- Swagger;
- Automapper;
- Repositoty and Unit-Of-Work;
- 3-Tier Layered architecture: DataAccess, BusinessLogic, API.

Enjoy!


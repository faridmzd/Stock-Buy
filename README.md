# Stock&Buy
 Simple product management system allowing users to perform CRUD (Create, Read, Update, Delete) operations on products.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)

## Overview

RESTful API is implemented using .NET 8, and the repository pattern is used for data access.
Entity Framework Core 8 is used as the Object-Relational Mapping (ORM) tool to interact with the SQLServer database.
Global exception handling is set up to catch and handle exceptions at a centralized level, providing consistent error management.

SQL DB Overview 
![image](https://github.com/faridmzd/Stock-Buy/assets/66147811/86903caf-82e7-4c77-8947-5dfd0ddf7377)


## Features

- Create, Read, Update, Delete products
- Find max number of given product that can be build
- Pagination
- Request validation - ( coming soon ) 
- GraphQL support - ( coming soon )

## Getting Started

- make sure you've dotnet ef tool installed. To install use : 
'dotnet tool install --global dotnet-ef' command.
- run 'dotnet ef database update'      (Because there is a migration file exists in project)

Note: Make sure that you're running the command inside Stock&Buy.API folder
You can use OpenAPI or Swagger to request various enpoints via premade templates by just navigation to '/swagger' path within browser.

## API Documentation
### Bundles

#### Get all bundles

```https
  GET /api/v1/bundles
```
| Parameter | Type     | 
| :-------- | :------- |
| `pageNumber`    | `int` | 
| `pageSize`      | `int` |  

**Response:**
```json
Status Code: 200 OK
{
  "items": [
    {
      "bundleId": "edd9e501-3ab9-44d4-9ca2-b453b38407b3",
      "name": "wheel"
    },
    {
      "bundleId": "12991c90-1d86-4a14-b936-d074384df6d7",
      "name": "bicycle"
    }
  ],
  "pageNumber": 1,
  "pageSize": 100,
  "totalPages": 1,
  "totalCount": 2
}
```
#### Get bundle

```https
  GET /api/v1/bundles/${id}
```

| Parameter | Type     | 
| :-------- | :------- |
| `id`      | `guid` |  

**Response:**
```json
Status Code: 200 OK
{
  "id": "12991c90-1d86-4a14-b936-d074384df6d7",
  "name": "bicycle",
  "associatedBundles": [
    {
      "id": "edd9e501-3ab9-44d4-9ca2-b453b38407b3",
      "name": "wheel",
      "quantity": 2,
      "associatedBundles": [],
      "associatedParts": [
        {
          "id": "41620ed0-27a0-4a61-9bb1-ca973bfe7d4e",
          "name": "frame",
          "quantity": 1
        },
        {
          "id": "dbf8bde5-b85e-43e0-b9b8-cd5ccff9d65e",
          "name": "tube",
          "quantity": 1
        }
      ]
    }
  ],
  "associatedParts": [
    {
      "id": "a4d256c6-2428-42f4-aea2-9b06a62b7af9",
      "name": "pedal",
      "quantity": 2
    }
  ]
}
```

#### Add bundle

```https
  POST /api/v1/bundles
```

| Body | Type     |
| :-------- | :------- | 
| `name`      | `string` |


**Request:**
```json
 {
  "name": "wheel"
}
```
**Response:**
```json
Status Code: 200 OK
{
  "bundleId": "1e5f8dfa-78e8-4f36-9aba-49f2557f7eef",
  "name": "wheel"
}
```

#### Delete bundle

```https
  DELETE /api/v1/bundles/${id}
```

| Parameter | Type     |
| :-------- | :------- |
| `id`      | `guid` |

**Response:**
```json
Status Code: 204 NO CONTENT
```

#### Update bundle

```https
  PUT /api/v1/tasks/${id}
```
| Parameter       | Type     |
| :-------- | :------- |
| `id`      | `guid` | 


| Body | Type     |
| :-------- | :------- |
| `name`      | `string` |


**Request:**
```json
 {
    "name":"frame"
  }
```

**Response:**
```json
Status Code: 204 NO CONTENT
```

#### Get max amount that bundle can be built

```https
  GET /api/v1/bundles/max/${id}
```

| Parameter | Type     | 
| :-------- | :------- |
| `id`      | `guid` |  

**Response:**
```json
Status Code: 200 OK
{
  17
}
```

#### Associate parts

```https
  POST /api/v1/bundles/associatedPart
```

| Body | Type     |
| :-------- | :------- | 
| `bundleId`      | `Guid` |
| `parts`      | `[]` |


**Request:**
```json
{
  "bundleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "parts": [
    {
      "partId": "12991C90-1D86-4A14-B936-D074384DF6D7",
      "quantity": 10
    },
    {
      "partId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 2
    }
  ]
}
```
**Response:**
```json
Status Code: 201 CREATED
{
}
```

#### Associate bundles

```https
  POST /api/v1/bundles/associatedBundle
```

| Body | Type     |
| :-------- | :------- | 
| `bundleId`      | `Guid` |
| `bundles`      | `[]` |


**Request:**
```json
{
  "bundleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "bundles": [
    {
      "bundleId": "12991C90-1D86-4A14-B936-D074384DF6D7",
      "quantity": 10
    },
    {
      "bundleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "quantity": 2
    }
  ]
}
```
**Response:**
```json
Status Code: 201 CREATED
{
}
```

#### Update associated part

```https
  PUT /api/v1/bundles/associatedPart/${id}
```
| Parameter       | Type     |
| :-------- | :------- |
| `id`      | `guid` | 


| Body | Type     |
| :-------- | :------- |
| `bundleId`      | `guid` |
| `associatedPartId`      | `guid` |
| 'quantity' | 'int' |

**Request:**
```json
{
  "bundleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "associatedPartId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "quantity": 0
}
```

**Response:**
```json
Status Code: 204 NO CONTENT
```

#### Update associated bundle

```https
  PUT /api/v1/bundles/associatedBundle/${id}
```
| Parameter       | Type     |
| :-------- | :------- |
| `id`      | `guid` | 


| Body | Type     |
| :-------- | :------- |
| `bundleId`      | `guid` |
| `associatedBundleId`      | `guid` |
| 'quantity' | 'int' |

**Request:**
```json
{
  "bundleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "associatedBundleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "quantity": 15
}
```

**Response:**
```json
Status Code: 204 NO CONTENT
```

### Parts

#### Get all parts

```https
  GET /api/v1/parts
```
| Parameter | Type     | 
| :-------- | :------- |
| `pageNumber`    | `int` | 
| `pageSize`      | `int` |  

**Response:**
```json
Status Code: 200 OK
{
  "items": [
    {
      "id": "a4d256c6-2428-42f4-aea2-9b06a62b7af9",
      "name": "pedal",
      "stockQuantity": 60
    },
    {
      "id": "41620ed0-27a0-4a61-9bb1-ca973bfe7d4e",
      "name": "frame",
      "stockQuantity": 60
    },
    {
      "id": "dbf8bde5-b85e-43e0-b9b8-cd5ccff9d65e",
      "name": "tube",
      "stockQuantity": 35
    }
  ],
  "pageNumber": 1,
  "pageSize": 100,
  "totalPages": 1,
  "totalCount": 3
}
```
#### Get part

```https
  GET /api/v1/parts/${id}
```

| Parameter | Type     | 
| :-------- | :------- |
| `id`      | `guid` |  

**Response:**
```json
Status Code: 200 OK
{
  "id": "a4d256c6-2428-42f4-aea2-9b06a62b7af9",
  "name": "pedal",
  "stockQuantity": 60
}
```

#### Add part

```https
  POST /api/v1/parts
```

| Body | Type     |
| :-------- | :------- | 
| `name`      | `string` |
| `stockQuantity`      | `int` |


**Request:**
```json
{
  "name": "tube",
  "stockQuantity": 35
}
```
**Response:**
```json
Status Code: 200 OK
{
  "id": "a4d256c6-2428-42f4-aea2-9b06a62b7af9",
  "name": "tube",
  "stockQuantity": 35
}
```

#### Delete part

```https
  DELETE /api/v1/parts/${id}
```

| Parameter | Type     |
| :-------- | :------- |
| `id`      | `guid` |

**Response:**
```json
Status Code: 204 NO CONTENT
```

#### Update part

```https
  PUT /api/v1/parts/${id}
```
| Parameter       | Type     |
| :-------- | :------- |
| `id`      | `guid` | 


| Body | Type     |
| :-------- | :------- |
| `name`      | `string` |
| `stockQuantity`      | `int` |

**Request:**
```json
{
  "name": "tube",
  "stockQuantity": 30
}
```

**Response:**
```json
Status Code: 204 NO CONTENT
```


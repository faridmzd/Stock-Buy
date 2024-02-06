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

# BookShelfAPI

Restful API that store your books and generate covers for them, simple as that.

Built with EF, Azure Blob Storage API and some random PDF Rasterizer that i found. 


## Actions

### Create a book

**Uri**: http://localhost:5000/api/books<br>
**Method**: POST<br>
**Format**: Multipart Form<br>
**Require**: 
- Title : string
- File: file

### List All Books

**Uri**: http://localhost:5000/api/books<br>
**Method**: Get<br>

### Get a Book

**Uri**: http://localhost:5000/api/books/{id}<br>
**Method**: Get<br>

### Update a Book - The api allows to update only the title

**Uri**: http://localhost:5000/api/books/{id}<br>
**Method**: PUT<br>
**Format**: Json<br>
**Require**: 
- Title : string

### Delete a Book

**Uri**: http://localhost:5000/api/books/{id}<br>
**Method**: Delete<br>

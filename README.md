Mini project: API
A simple web API with REST architecture where you can:
- Store persons with basic information such as their firstname, lastname and phonenumber.
- Store several interests that the persons have. Each interest has a title and a short description.
- Store several interests per person.
- Store several links to each interest a specific person has. The link is then connected to both the person and that interest.

Calls made to the API through Insomnia:
1. Get all persons in the system: `GET http://localhost:5108/persons`
   ```
   [
	{
		"firstName": "Charlotte",
		"lastName": "Andersson",
		"phoneNumber": "0722999999"
	},
	{
		"firstName": "Anders",
		"lastName": "Johansson",
		"phoneNumber": "0739222222"
	},
	{
		"firstName": "Maja",
		"lastName": "Lundqvist",
		"phoneNumber": "0737000000"
	},
	{
		"firstName": "Erik",
		"lastName": "Svensson",
		"phoneNumber": "0708777777"
	}
   ]
2. Get all interests a specific person has: `GET http://localhost:5108/persons/1/interests`
   ```
   [
	{
		"title": "Dance",
		"description": "Dance is an art form, often classified as a sport, consisting of sequences of body movements with aesthetic and often symbolic value, either improvised or purposefully selected."
	},
	{
		"title": "Movies",
		"description": "Movies are a work of visual art that simulates experiences and otherwise communicates ideas, stories, perceptions, feelings, beauty, or atmosphere through the use of moving images. These images are generally accompanied by sound and, more rarely, other sensory stimulations."
	}
   ]
   ```
3. Get all links a specific person has: `GET http://localhost:5108/persons/1/links`
   ```
   [
	{
		"linkToPage": "https://en.wikipedia.org/wiki/Ballet"
	},
	{
		"linkToPage": "https://www.britannica.com/art/reggaeton"
	},
	{
		"linkToPage": "https://www.imdb.com/"
	}
   ]
   ```
4. Connect a person to a new interest: `POST http://localhost:5108/persons/1/interests`
   With the following body:
   ```
   {
	"title": "Knitting",
	"Description": "Knitting is a method for production of textile fabrics by interlacing yarn loops with loops of the same or other yarns. It is used to create many types of garments. Knitting may be done by hand or by machine."
   }
   ```
   If we now again call: `GET http://localhost:5108/persons/1/interests`
   We get:
   ```
   [
	{
		"title": "Dance",
		"description": "Dance is an art form, often classified as a sport, consisting of sequences of body movements with aesthetic and often symbolic value, either improvised or purposefully selected."
	},
	{
		"title": "Movies",
		"description": "Movies are a work of visual art that simulates experiences and otherwise communicates ideas, stories, perceptions, feelings, beauty, or atmosphere through the use of moving images. These images are generally accompanied by sound and, more rarely, other sensory stimulations."
	},
	{
		"title": "Knitting",
		"description": "Knitting is a method for production of textile fabrics by interlacing yarn loops with loops of the same or other yarns. It is used to create many types of garments. Knitting may be done by hand or by machine."
	}
   ]
   ```
5. Add new link to a specific person as well as a specific interest: `POST http://localhost:5108/persons/1/1004/links`
   With the following body:
   ```
   {
	"linkToPage": "https://sheepandstitch.com/"
   }
   ```
   If we now again call: `GET http://localhost:5108/persons/1/links`
   We get:
   ```
   [
	{
		"linkToPage": "https://en.wikipedia.org/wiki/Ballet"
	},
	{
		"linkToPage": "https://www.britannica.com/art/reggaeton"
	},
	{
		"linkToPage": "https://www.imdb.com/"
	},
	{
		"linkToPage": "https://sheepandstitch.com/"
	}
   ]
   ```

ER diagram for the database:

![ER diagram API project](https://github.com/chasweley/Mini-project-API/assets/123236297/70dae799-bcb2-4afb-923d-408191a73b70)

UML diagram for the API:

![UML diagram API(1)](https://github.com/chasweley/Mini-project-API/assets/123236297/2e02d230-c2bd-4d78-8d3b-f924396ec50a)



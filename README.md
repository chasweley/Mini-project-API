Mini project: API
A simple web API with REST architecture where you can:
- Store persons with basic information such as their firstname, lastname and phonenumber.
- Store several interests that the persons have. Each interest has a title and a short description.
- Store several interests per person.
- Store several links to each interest a specific person has. The link is then connected to both the person and that interest.

Calls made to the API through Insomnia:
1. `http://localhost:5108/persons`
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
2. `http://localhost:5108/persons/1/interests`
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

ER diagram for the database:

UML diagram for the API:

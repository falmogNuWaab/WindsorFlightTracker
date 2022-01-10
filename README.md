# WindsorFlightTracker
## Description:
A small MVC Project with a C# backend and ASP .Net MVC (Asp .Net 3.1) front-end. This project was started to provide the Windsor Air Cadets a way to monitor Air Traffic over Windsor Ontario and Detroit Michigan. This application pulls flight data from Opensky-Network anonymously and places corresponding markers on a map. I am using LeafletJS & Mapbox for the map and map functionality. I am also using Newtonsoft.Json to deserialize the response from OpenSky.


## Using this project:
Feel free to fork this repository and modify the code to work for you. I will try my best to give you the information you will need to customize this to fit your needs. For questions about the API's please use the links below as a starting point.

OpenSky - https://opensky-network.org/

LeafLetJS - https://leafletjs.com/

MapBox - https://www.mapbox.com/

IMPORTANT! To use Leaflet, you will need a MapBox API key. I signed up for a free account and that worked fine for what I need. In my code I used a secret class that will add the API key as a property in the Traffic Model. You can do the same, or you can just hardcode your API key into the class. 

## Models
You will find all models in Traffic.cs.

#### RawData Model: 
This is a temporary model that gets populated with the raw data from Opensky. Opensky delivers the data in JSON format. It will return a timestamp and an object array with all "vectors" or properties describing each plane over Windsor.

#### Traffic Model: 
This contains a timestamp, an array of "Flights", and a string that will house the Mapbox API key.

#### Flight Model: 
This model represents the individual aircraft's current state. I didn't keep all properties received from the RawData. I had no need for Geo_Altitude, for instance. If there is information you would like to include in the Flight model, make sure you make the necessary adjustments in the OpenSkyDAL methods.

#### OpenSkyDAL Model: 
This is the DataAccess Layer class that interacts with OpenSky's network. This class contains the methods that you will need to modify to pull data relevant to your area.

## OpenSkyDAL Methods:
#### GetAllFlightsAboveWindsor(): 
This method gets all flights from above Windsor Ontario. IF you don't want to see flights above Windsor Ontario, you will need to get the "bounding box" information for the area and modify the string url variable accordingly. This pulls data anonymously from OpenSky. No authentication is necessary. If you are feeding data to OpenSky and would like to access your own Data, this is where you'd modify the URL. Authentication will be necessary, but OpenSky's documentation will walk you through that.

#### CreateFlightStates(): 
This Method converts the raw data received from GetAllFlightsAboveWindsor() into the Traffic and Flight models so that they can be easily used on the front-end. You will notice I convert some numbers to strings, that is for the popup box when a plane is clicked on the map. 

## Controllers
#### HomeController: 
Nothing special going on here. You will notice I create an OpenSkyDAL variable that I use to call the CreateFlightStates() Method to supply data to the View.

#### FlightController: 
This is the controller for the Flights/ folder and views. This one also has an OpenSkyDAL variable that grants the controller access to the OpenSkyDAL methods.

## Views
Home/Index.cshtml & Flights/Index.cshtml are identical. Either of those views will display flights over Windsor, Ontario. 
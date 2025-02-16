<h2>A ASP.NET Web API for a Vehicle Assembly Scenario</h2> 

In an automotive manufacturing facility, a dedicated production line assembles vehicles for a specific brand. Each vehicle on the production line has a predefined color and engine type. The assembly process is carried out by workers assigned to different tasks on the production line.

Each worker can be responsible for assembling multiple vehicles, and each vehicle requires contributions from multiple workers to complete the assembly process. The system tracks the assembly activities by recording:

<ul>
  <li>Which worker is assigned to which vehicle(s)</li>
  <li>The date on which a worker performed assembly on a vehicle</li>
  <li>Whether the assembly task assigned to the worker is completed or still in progress</li>
</ul>

<h2>The ER Diagram for the System</h2> 
<img src="https://github.com/user-attachments/assets/1417c1bd-0d23-48a0-b9a6-97c16cb8dc48" width="700" height="480">

<h2>The Relational Schema for the System</h2> 
<img src="https://github.com/user-attachments/assets/97cf9c95-2a3e-4dc7-9677-bcdeff620bc1" width="700" height="480">

<h2>To Config</h2> 
```
docker compose up --build

```

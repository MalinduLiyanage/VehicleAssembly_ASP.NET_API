<h2>A ASP.NET Web API for a Vehicle Assembly Scenario</h2> 

In an automotive manufacturing facility, a dedicated production line assembles vehicles for a specific brand. Each vehicle on the production line has a predefined color and engine type. The assembly process is carried out by workers assigned to different tasks on the production line.

Each worker can be responsible for assembling multiple vehicles, and each vehicle requires contributions from multiple workers to complete the assembly process. The system tracks the assembly activities by recording:

<ul>
  <li>Which worker is assigned to which vehicle(s)</li>
  <li>The date on which a worker performed assembly on a vehicle</li>
  <li>Whether the assembly task assigned to the worker is completed or still in progress</li>
</ul>

<h2>The ER Diagram for the System</h2> 
<img src="https://github.com/user-attachments/assets/1417c1bd-0d23-48a0-b9a6-97c16cb8dc48">

<h2>The Relational Schema for the System</h2> 
<img src="https://github.com/user-attachments/assets/97cf9c95-2a3e-4dc7-9677-bcdeff620bc1">

<h2>SQL Create Table Queries</h2> 

```
CREATE TABLE vehicle (
    vehicle_id INT PRIMARY KEY auto_increment,
    model VARCHAR(100) NOT NULL,
    color VARCHAR(50) NOT NULL,
    engine VARCHAR(50) NOT NULL
);

CREATE TABLE worker (
    nic INT PRIMARY KEY NOT NULL,
    firstname VARCHAR(50) NOT NULL,
    lastname VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL,
    address VARCHAR(50) NOT NULL,
    job_role VARCHAR(50) NOT NULL
);

CREATE TABLE assembles (
    nic INT NOT NULL,
    vehicle_id INT NOT NULL,
    date DATE NOT NULL,
    isCompleted BOOLEAN NOT NULL DEFAULT FALSE,
    PRIMARY KEY (nic, vehicle_id),
    FOREIGN KEY (nic) REFERENCES worker(nic),
    FOREIGN KEY (vehicle_id) REFERENCES vehicle(vehicle_id)
);


```

# Zoolandia HTML

## Setup

1. Create a new solution in Visual Studio named `ZoolandiaWeb`
1. Using the ERD you created in [draw.io](https://www.draw.io/), add a database to your project and go through the process of creating the tables and foreign keys.
1. Either manually enter in data, or write SQL statements to insert it.

## Instructions

Using the SimpleServer project as a baseline, you need to write produce the following HTML responses about Zoolandia.

1. `/animals` - This will produce a simple HTML list of all animals in the database. You should display the following information about each animal.
    1. Animal name (this information will be an anchor to take the user to the specific animal view)
    1. Current habitat in the zoo
1. `/animals/1` - When the `id` of an animal is in the URL, display information about that specific animal.
    1. Name
    1. Species common name (if exists)
    1. Species scientific name
    1. Current habitat
    1. Age of animal
1. `/habitats` - This will list all habitats open for public visitation.
    1. Habitat name
    1. How many animals currently in the habitat
1. `/habitats/1` - Display information about this specific habitat.
    1. Habitat name
    1. Habitat type
    1. List the names of the animals currently in the habitat
    1. List the employees currently assigned to maintenance of the habitat
1. `/employees/` - List all employees
1. `/employees/1` - Show employees name, age, and habitats currently assigned to
This program saves recipes using several abstractions helpful for creating a menu.

Recipes- a note with zero or more instructions, ingredients, and seasons.
Plating- a plating style. Each recipe can have zero or more plating styles.
Larder- A recipe that is used in other recipes. Typically made in bulk and can keep for a while.

Program can make and modify the associations listed above.
Recipes, larder items, plating styles, ingredients, and instructions have CRUD functionality.

ENTITY MODELS
Class inheritance is as follows in Larder.Data.Models.

BASE			   DERIVED   DERIVED
Food (abstract) => Larder => Recipe
				=> Ingredient
				=> Plating
Action
Season
RecipePlating (intermediate table)

Class inheritance in Larder.Models mirrors this structure.

DATABASE RELATIONSHIPS
Larder => Ingredient | One to many
Larder => Action     | One to many
Larder => Season     | One to one
Recipe => Plating    | Many to many



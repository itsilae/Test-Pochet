## Test technique

Le test consiste à réaliser une API REST .Net permettant de :

- Créer des films.
- Lister les films.

## Qu'est-ce qu'un film ?

Un film est caractérisé par :

- Un titre.
- Une année de sortie.
- Un réalisateur ou une réalisatrice.
- Une liste d'acteurs et d'actrices.
- Une liste de genres (valeurs possibles : `Action`, `Comédie`, `Drame`, `Horreur` et `Science-fiction`).
- Un budget en dollars.

## Qu'est-ce qu'un réalisateur ou une réalisatrice ?

Un réalisateur ou une réalisatrice est caractérisé par :

- Un nom de famille.
- Un prénom.

## Qu'est-ce qu'un acteur ou une actrice ?

Un acteur ou une actrice est caractérisé par :

- Un nom de famille.
- Un prénom.

## Création d'un film

Avant de persister le film, il faut vérifier qu'il soit valide.

Règles de validation d'un film :

- Un film ne peut pas avoir une année de sortie dans le futur.
- Un film ne peut pas avoir été réalisé avant 1895.
- Le titre est obligatoire.
- Le réalisateur ou la réalisatrice est obligatoire.
- Le budget du film doit être supérieur à 0.
- Un film ne peut pas avoir la même combinaison de titre, année de sortie et réalisateur ou réalisatrice qu'un autre film déjà créé.

## Liste des films

L'API doit permettre de : 

- Liste des films d'un réalisateur ou d'une réalisatrice donné.
- Trier les films par titre, année de sortie ou budget,
- Afficher toutes les propriétés du film. La liste des acteurs et des actrices doit apparaître sous la forme `{FirstName} {LastName}` (par exemple : "Michelle Yeoh").

## Stockage

Les données doivent être persistées dans MySQL via Entity Framework.

La table des réalisateurs et des réalisatrices doit être initialisée avec les réalisateurs et réalisatrices suivants :

- Spielberg Steven.
- Bigelow Kathryn.
- Tarantino Quentin.

La table des acteurs et des actrices doit être initialisée avec les acteurs et actrices suivants :

- Yeoh Michelle.
- Pitt Brad.
- Ortega Jenna.

## Test unitaires

Les règles de validation du film doivent être testées unitairement (avec xUnit).

## Notes

- Pas besoin d'authentification.
- Aucune interface utilisateur requise.

## Utilisation de librairies

Comme tout développeur, nous n'aimons pas réinventer la roue, et apprécions de ce fait utiliser diverses bibliothèques selon les besoins.

Cependant ce test nous permet d'évaluer comment vous abordez un problème et le résolvez. Par conséquent, nous préférons que vous limitiez l'utilisation de bibliothèques dans l'application (vous pouvez si vous le souhaitez indiquer les bibliothèques que vous auriez aimé utiliser). Cette règle ne s'applique toutefois pas aux tests unitaires pour lesquels vous pouvez utiliser les librairies de votre choix, en plus d'xUnit.

 ## Critères d'évaluation
 
Vous serez notamment évalué sur la maintenabilité de votre code (lisibilité, extensibilité, modularisation, homogénéité, etc.), ainsi que sur votre capacité à répondre au cahier des charges, quitte à prendre des décisions et à savoir les justifier si certains points vous semblent peu clairs.

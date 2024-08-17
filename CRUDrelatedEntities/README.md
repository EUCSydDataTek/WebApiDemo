# 4. CRUDrelatedEntites

## Demo af POST af graph
Indsæt følgende som Request Body

    {
      "name": "Egon Rasmussen",
      "pluralsightUrl": "https://analogteknik.com",
      "twitterAlias": "analog",
      "books": [
        {
          "title": "Analogteknik",
          "publisher": "Industriens Forlag"
        }
      ]
    }

## Demo af GET /api/Authors
Se følgende som Response Body:

    [
      {
        "authorId": 1,
        "name": "Martin Fowler",
        "pluralsightUrl": "https://app.pluralsight.com/profile/martin-fowler",
        "twitterAlias": "https://twitter.com/martinfawler"
      },
      {
        "authorId": 2,
        "name": "Eric Evans",
        "pluralsightUrl": "https://app.pluralsight.com/profile/eric-evans",
        "twitterAlias": "https://twitter.com/ericevans"
      },
      {
        "authorId": 3,
        "name": "Steve Smith",
        "pluralsightUrl": "https://app.pluralsight.com/profile/steve-smith",
        "twitterAlias": "https://twitter.com/stevesmith"
      },
      {
        "authorId": 4,
        "name": "Egon Rasmussen",
        "pluralsightUrl": "https://analogteknik.com",
        "twitterAlias": "analog"
      }
    ]

## Demo af GET /api/Authors/4
Se følgende som Response Body:

    {
      "authorId": 4,
      "name": "Egon Rasmussen",
      "pluralsightUrl": "https://analogteknik.com",
      "twitterAlias": "analog",
      "books": [
        {
          "bookId": 5,
          "title": "Analogteknik",
          "publisher": "Industriens Forlag"
        }
      ]
    }

## Demo af PUT /api/Authors/4

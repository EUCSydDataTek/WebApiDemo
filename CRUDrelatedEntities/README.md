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

## Demo af GET /api/Blogs
Se følgende som Response Body:

    [
      {
        "BlogId": 1,
        "name": "Martin Fowler",
        "pluralsightUrl": "https://app.pluralsight.com/profile/martin-fowler",
        "twitterAlias": "https://twitter.com/martinfawler"
      },
      {
        "BlogId": 2,
        "name": "Eric Evans",
        "pluralsightUrl": "https://app.pluralsight.com/profile/eric-evans",
        "twitterAlias": "https://twitter.com/ericevans"
      },
      {
        "BlogId": 3,
        "name": "Steve Smith",
        "pluralsightUrl": "https://app.pluralsight.com/profile/steve-smith",
        "twitterAlias": "https://twitter.com/stevesmith"
      },
      {
        "BlogId": 4,
        "name": "Egon Rasmussen",
        "pluralsightUrl": "https://analogteknik.com",
        "twitterAlias": "analog"
      }
    ]

## Demo af GET /api/Blogs/4
Se følgende som Response Body:

    {
      "BlogId": 4,
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

## Demo af PUT /api/Blogs/1

    {
      "blogId": 1,
      "url": "http://blog1.com",
      "rating": 1,
      "posts": [
        {
          "postId": 3,
          "title": "Updated Title",
          "content": "Updated Content",
          "rating": 1
        }
      ]
    }
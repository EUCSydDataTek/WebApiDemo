# 4. CRUDrelatedEntites

Her er Blog-projektet udvidet med en relateret Post-klasse. Dette er en 1 til mange relation, hvor en Blog kan have mange Posts. 

DTO-klasserne er udvidet med en `BlogPostDto`-klasse, samt en `CreateBlogPostDto`-klasse.

&nbsp;

## Demo af POST af graph
Indsæt følgende som *Request Body* i en POST:

    {
        "url": "https://blog5.com",
        "rating": 2,
        "posts": [
            {
                "title": "Dette er en helt ny Post",
                "content": "Dette er en ny Post indsat i en ny Blog",
                "rating": 3
            }
        ]
    }

&nbsp;

## Demo af GET /api/Blogs
Se nu følgende som *Response Body* og bemærk blogId = 5, som er ny:

    [
      {
        "blogId": 1,
        "url": "https://blog1.com",
        "rating": 2
      },
      {
        "blogId": 2,
        "url": "https://blog2.com",
        "rating": 3
      },
      {
        "blogId": 3,
        "url": "https://blog3.com",
        "rating": 1
      },
      {
        "blogId": 4,
        "url": "https://blog5.com",
        "rating": 3
      },
      {
        "blogId": 5,
        "url": "https://blog5.com",
        "rating": 2
      }
    ]

   &nbsp;

## Demo af GET /api/Blogs/5
Nu vises det nye Blog-objekt med relateret Post-objekt som *Response Body*:

    {
      "blogId": 5,
      "url": "https://blog5.com",
      "rating": 2,
      "posts": [
        {
          "postId": 7,
          "title": "Dette er en helt ny Post",
          "content": "Dette er en ny Post indsat i en ny Blog",
          "rating": 3
        }
      ]
    }

 &nbsp;

## Demo af PUT /api/Blogs/1
Send følgende som *Request Body* til BlogId = 1, som indeholder opdaterede data for både Blog og Post:

    {
      "blogId": 1,
      "url": "https://blog1.com",
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

 Test med GET /api/Blogs/1 for at se at graphen er opdateret.   

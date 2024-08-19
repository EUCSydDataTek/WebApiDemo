# 4. CRUDrelatedEntites

Her er Blog-projektet udvidet med en relateret Post-klasse. Dette er en 1 til mange relation, hvor en Blog kan have mange Posts. 

DTO-klasserne er udvidet med en `BlogPostDto`-klasse, samt en `CreateBlogPostDto`-klasse.

&nbsp;

## Demo af POST af graph
Inds�t f�lgende som *Request Body* i en POST:

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
Se nu f�lgende som *Response Body* og bem�rk blogId = 5, som er ny:

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
Send f�lgende som *Request Body* til BlogId = 1, som indeholder opdaterede data for b�de Blog og Post:

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

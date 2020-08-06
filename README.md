# ShrtLy

This repository contains a refactoring kata. It's supposed to simulate the takeover and delivery of new features in a legacy project.

The company you recently started working for, had accumulated over the years a lot of websites, many of which have very long paths, and use inconsistent conventions to defining the routes.

A while ago one of the junior developers wanted to be able to share links to websites with his colleagues so he started working on an application that would shorten the links he wanted to share the links would fit better in chats and emails. Unfortunately the original developer has left the company before he could finish this service.

Now one of the managers wants this service to be able to share pretty links with clients, and he asked you to get this application up and running.

## Functional requirements

- The application should be able to accept URLs and shorten them. The shorter, the better.
- The application should be able to redirect users to the original URL when they use a shortened link.
- The application should not accept already shortened URLs. Cause why would it.
- The application should return the existing link when the URL is already shortened
- The manager wants to add the possibility for the registered users to view their created links
- The manager wants the registered users to be able to create custom links (e.g. `https://shrt.ly/my-custom-link`)

## Non-functional requirements

- The manager expects to use the application to generate A LOT of link so it should remain fast even after a lot of URLs have already been shortened.
- The application must be super reliable so the clients do not complain about not being able to use the links.
- The manager wants to deploy the application in the cloud since the manager heard that's cheap.

## Rules

Feel free to make any changes you think are necessary to the code. 
The manager wants this application to be ready in a few days, so the less time you spend the better.

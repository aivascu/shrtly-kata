# ShrtLy

This repository contains a refactoring kata, that's supposed to simulate the takeover and delivery of new features on a legacy .NET project, in a corporate environment.
The solution contains a slew of bad practices, and bugs commonly encountered in legacy .NET code.

> [!NOTE]  
> Any coincidence with real world projects, organizations, people, and situations is purely accidental.

## Scenario

The company you recently started working for, had accumulated over the years a lot of websites, many of which have very long paths, and use inconsistent conventions to defining the routes.

A while ago one of the junior developers at your company, wanted to be able to share links to websites with his colleagues so he started working on an application that would shorten the links he wanted to share the links would fit better in chats and emails. The links eventually started being shared with the clients of your company, and are now an indespensible part of your company's platform.

Sadly the quality of the code started showing, and now one of the managers wants this service fixed, and upgraded with a few new features.
Unfortunately the original developer has left the company a while ago, and now it's your job to pick up the pieces and continue working on this service.

### Functional requirements

- The application should be able to accept URLs and shorten them. The shorter, the better.
- The application should be able to redirect users to the original URL when they use a shortened link.
- The application should return the existing link when the URL is already shortened
- The application should not accept already shortened URLs. Cause why would it.
- The manager wants to add the possibility for the registered users, to view their created links
- The manager wants the registered users to be able to create custom links (e.g. `https://<your domain>/my-custom-link`)

### Non-functional requirements

- The manager expects to use the application to generate A LOT of links, so it should remain fast even after a lot of URLs have already been shortened.
- The application must be super reliable so the clients do not complain about not being able to use the links.
- The manager told you that the IIS server where the app used to be hosted is getting decomissioned soon, so you should probably make sure your code can be deployed to the organization's Kubernetes cluster.
- Oh, and the manager also told you he has some big plans for this project, so expect more requirements coming in soon.

## Rules

Feel free to make any changes to the code, that you think are necessary.
The manager wants this application to be ready for a demo in a couple days, so you should prioritize accordingly.

# DevNet

<h3>Project Description</h3>

<p style="color: #999999; background-color: rgba(0,0,0,.6);">
    This project will be a web application called “DevNet”.  The typical user will be a software engineer or developer.  They will first register or login to the site.  The user will then be presented with a dark themed site employing modern web design techniques including three dimensional graphics in the look and feel.  There will be a video gallery for demos and tutorials, industry related RSS feeds, and finally a dashboard of various data points gathered from data mining.  There will also be the standard social networking links to Twitter, Facebook, LinkedIn, etc.  As far as CRUD functionality, the user will create their profile when they first register to the site.  They can later edit their profile or delete it if they choose.  The CRUD operations would also extend to the site’s content tied to that user.
</p>

<h3>Solutions Provided</h3>
<p style="color: #999999;background-color: rgba(0,0,0,.6);">
    One benefit of this site will be a one stop place for developers to network.
    This can also be a good place for employers to fill their software positions with talent.
    This will save the companies time and money on hiring costs.
    The second major benefit is from the data mining results.
    This could help the developer decide in advance which technologies they should invest their time.
    The data mining works by making predictions based on historical data by using mining algorithms.
    This would also be a useful tool for businesses so they can make more intelligent purchasing decisions when it comes to technology spending and investment.
</p>
<h3>Technologies</h3>
<p style="color: #999999;background-color: rgba(0,0,0,.6);">
    The main technologies that will be used for the site are ASP.NET MVC C# using the Razor view engine for the server-side and JavaScript for the client-side.  The database will be MS SQL Server. In addition, I will be using the data mining tools that are included in Microsoft SQL Server Analysis Services.  The View will be HTML 5, CSS 3, and Three.js framework for the three dimensional graphics and possibly D3.js for data visualization for the dashboard.
</p>

<h3>Finished Application</h3>
<p style="color: #999999;background-color: rgba(0,0,0,.6);">
    The main idea of this project is to have a social networking site that is oriented to the needs and interests of developers as well as employers looking to hire developers.  The goal of the site will be a place to share tutorials and project demonstrations or use case scenarios.  In addition, data mining techniques will be employed to predict technology usage depending on such factors as a developer’s age, income, or location.  Finally, the site will have an updated look and feel using three dimensional graphics.  The site will comprise of three areas.  There will be a login area, content area, and a dashboard area.
    The login area will just be your standard login but will provide an initial impression of the site’s look and feel as well as brand.  The site will be called, “DevNet” and there will be a distinct logo.  A new user will register for the site.  They will fill out a form which will provide their initial profile for the site.  This profile will consist of their first name, last name, user name, password, date of birth, job title, where they are currently employed, city, state, as well as some general questions with regards to technology preferences.  There may be a separate “survey section” instead for gather data about the user’s technology preferences.
    The content area will be broken into separate areas of the site with links in the navigation menu.  The separate content areas will consist of a YouTube gallery where each user can upload a tutorial or demonstration and a panel on the side with RSS feeds that would be of interest to developers.  Also there will be links in the footer to social networking sites.  The YouTube videos will be displayed as if they are floating in a three dimensional space. Some will appear close to the front of the screen and others will recede into the background.  The user can navigate through this space to select a video.  For the purpose of having some initial content for fictitious site members, I will probably just use programming related YouTube videos that already exist.
    The dashboard area will display visualizations of the results of various data mining algorithms from Microsoft SQL Analysis Services.  In order to obtain enough records to perform such an analysis, I will write some SQL to auto generate the inserts.  I will probably need at least three hundred but one thousand inserts would be better.  There is also the possibility of using external data sets as well although I am not as certain how to make use of them in Analysis Services.  It would be best to use various mining models.  The main ones are generated from Microsoft Decision Tree Model, Microsoft Clustering Model, and the Microsoft Naïve Bayes Model.  The best model can then be tested against the testing data set to determine which one predicts most accurately.  Once the query is defined in Microsoft SQL Server Analysis by using Prediction Query Builder, the results can be saved to a new or existing table in the database.
</p>

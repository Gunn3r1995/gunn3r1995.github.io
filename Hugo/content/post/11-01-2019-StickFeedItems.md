---
layout:     post 
title:      "Returning Negative and Positive Numbers"
subtitle:   "The hunt for the sticky"
date:       2019-01-11
author:     "Shane Smith"
image:      "https://img.zhaohuabing.com/post-bg-2015.jpg"
---

# Returning Negative and Positive Numbers

## Abstract

Due to the calculations of a piece of work I required to do the following check `[Appear Date] >= DateTime.Now.AddDays(-X)` where X is the amount of days to add. This determined whether the item supplied should be 'sticky' and therefore at the top of a feed or not.

*For Example:*

```
10/01/2019 13:00:00 >= (11/01/2019 14:57:37 - 3 Days) == true; // Sticky
05/01/2019 13:00:00 >= (11/01/2019 14:57:37 - 3 Days) == false; // Not Sticky
```

However, for readability I wanted `X` the number of days to be sticky to be a negative number, even if supplied with a positive number. In fact it makes more sense to the end user providing a positive number in; I want the sticky feed items to be sticky for 5 days. This was due to way the calculation worked...

If I was to do the following, the negation would not be needed. However, I was doing this query within a CAML query; and did not currently have the value of the appear date, so DateTime.Now was the easiest option.

```csharp
([AppearDate]).AddDays(X) <= DateTime.Now
```

## The Solution

So I opened up my LINQPad and used `System.Math.Abs` which produces an absolute (I.E positive version) of the supplied number and then I multiply it by `-1`. Thus always return a negative number.

### Always return a negative number

```C sharp
var negativeNumber = -25;
var positiveNumber = 25;

var negativeResult = System.Math.Abs(negativeNumber) * (-1);
Console.WriteLine(negativeResult); // returns -25

negativeResult = System.Math.Abs(positiveNumber) * (-1);
Console.WriteLine(negativeResult); // returns -25
```

```c sharp
// Outputs
> -25
> -25
```

### Quick note: always return a positive number

```c sharp
var positiveResult =  System.Math.Abs(negativeNumber);
Console.WriteLine(positiveResult); // returns 25

positiveResult = System.Math.Abs(positiveNumber) * (-1);
Console.WriteLine(positiveResult);
```

``` c sharp
// Outputs
> 25
> 25
```

<div class='daily-stand-up'>
    <h6 class='daily-stand-up-title'>Daily Stand-Up</h6>
    <div><p><strong>Yesterday I worked on...</strong></p></div>
    <div>
        <ul>
            <li>The final improvements for the Feature Notes Builder.</li>
            <li>Discussed how added links to the Test Cases* and capturing them within.</li>
            <li>Had my year end appraisal.</li>
        </ul>
        <p>* The test scripts within TFS that are written by the test team.</p>
    </div>
    <div><p><strong>Today I am going to...</strong></p></div>
    <div>
        <ul>
            <li>Adding a All Property for the configurable amount of days a feed item are sticky for.</li>
            <li>See if I can resolve my K2 Issue. (SharePoint Claims based cannot find the K2 token within the database).</li>
        </ul>
    </div>
<div>
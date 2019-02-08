---
layout:     post 
title:      "Creating the blog"
subtitle:   "what do I do"
date:       2019-01-12
author:     "Shane Smith"
# image:      ""
tags: 
    - General
categories: [ Life ]
---

# Creating the blog

Today I was researching in the best and path of least resistance for creating and hosting this blog.

The easy part was hosting; [GitHub pages](https://pages.github.com/), it's free and provide full flexibility.

I wanted to use [Hugo](https://gohugo.io/) as it allows for very customisable themes (I could create my own in the future). But right now I wanted a quick solution. I could potential use one of them blogging sites like [wordpress](https://wordpress.com), but that's boring.

## The Plan

So the plan for now goes like*:

1. Set-up a hugo site **template**; This will have all the boilerplate.
2. Write a couple of blog posts using markdown (what I'm doing currently).
3. Write the console application to copy & paste the markdown to the hugo site.
4. Run Hugo like: `> Hugo`.
5. Run a small copy & past script to copy the outputted `public` folder to the location of my GitHub pages site.

> \* Eventually instead of manually doing the above, it will be completed automatically, either by online tools or as a scheduled task.
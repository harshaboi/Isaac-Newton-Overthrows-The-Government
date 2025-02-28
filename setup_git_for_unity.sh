#!/bin/bash

# Prompt for GitHub username and repo name
echo "Enter your GitHub username:"
read GITHUB_USER

echo "Enter your GitHub repository name:"
read REPO_NAME

# Check if Git is initialized
if [ ! -d ".git" ]; then
    echo "Initializing Git repository..."
    git init
else
    echo "Git is already initialized."
fi

# Add Unity .gitignore if not present
if [ ! -f ".gitignore" ]; then
    echo "Downloading Unity .gitignore..."
    curl -o .gitignore https://raw.githubusercontent.com/github/gitignore/main/Unity.gitignore
    echo ".gitignore added."
fi

# Add the remote repository
git remote add origin https://github.com/$GITHUB_USER/$REPO_NAME.git 2>/dev/null

# Check if remote is set
git remote -v | grep origin
if [ $? -ne 0 ]; then
    echo "Remote repository not set correctly. Please check your repository URL."
    exit 1
fi

# Add, commit, and push initial files
echo "Adding files to Git..."
git add .
git commit -m "Initial commit"

echo "Pushing to GitHub..."
git branch -M main
git push -u origin main

echo "Setup complete! Use 'git pull' and 'git push' to sync changes."

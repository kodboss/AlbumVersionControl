﻿using System;
using GitApi.Interfaces;
using Octokit;

namespace AlbumVersionControl.Models.GitHubApi
{
    public class GitHubCommit : GitHubObject, IGitCommit
    {
        public GitHubCommit(GitHubClient client)
        {
            Client = client;
        }

        public GitHubCommit(GitHubClient client, IGitRepository repository) : this(client)
        {
            Repository = repository;
        }

        public GitHubCommit(GitHubClient client, string message, string sha) : this(client)
        {
            Message = message;
            Sha = sha;
        }

        public GitHubCommit(GitHubClient client, string message, string sha, IGitRepository repository) : this(client, message, sha)
        {
            Repository = repository;
        }

        public string Message { get; set; }

        public string Sha { get; set; }

        public string Author { get; set; }

        public DateTime CreatedAt { get; set; }

        public IGitRepository Repository { get; set; }

        public void Map(global::Octokit.GitHubCommit commit)
        {
            if (Repository == null)
            {
                Repository = new GitHubRepository(Client);
                if (Repository is GitHubRepository gitHubRepository)
                {
                    gitHubRepository.Map(commit.Repository);
                }
            }

            Message = commit.Commit?.Message;
            Sha = commit.Sha;
            Author = commit.Author?.Login;
            if (commit.Commit != null) CreatedAt = commit.Commit.Committer.Date.DateTime;
        }
    }
}
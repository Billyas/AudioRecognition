using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using AudioRecognition.Model;

namespace AudioRecognition.DAL
{
    public class ASRContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FlashRecognitionResult> FlashRecognitionResults { get; set; }
        public DbSet<ShortRecognitionResult> shortRecognitionResults { get; set; }
        public DbSet<LiveRecognitionResult>  liveRecognitionResults { get; set; }

        public string DbPath { get; private set; }

        public ASRContext()
        {
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            var currentfolder = Environment.CurrentDirectory;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}blogging.db";
            DbPath = $"{currentfolder}\\Data.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }


}
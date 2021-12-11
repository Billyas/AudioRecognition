/*
Navicat SQLite Data Transfer

Source Server         : Data
Source Server Version : 30808
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30808
File Encoding         : 65001

Date: 2021-12-11 19:56:18
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for FlashRecognitionResults
-- ----------------------------
DROP TABLE IF EXISTS "main"."FlashRecognitionResults";
CREATE TABLE "FlashRecognitionResults" (
"Request_id"  TEXT NOT NULL,
"Audio_duration"  TEXT,
"Message"  TEXT,
"Flash_result"  TEXT,
"Time"  TEXT NOT NULL,
"Username"  TEXT,
PRIMARY KEY ("Request_id" ASC),
CONSTRAINT "FK_FlashRecognitionResults_Users_Username" FOREIGN KEY ("Username") REFERENCES "Users" ("Username") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Table structure for LiveRecognitionResults
-- ----------------------------
DROP TABLE IF EXISTS "main"."LiveRecognitionResults";
CREATE TABLE "LiveRecognitionResults" (
"Voice_id"  TEXT NOT NULL,
"Message"  TEXT,
"MessageId"  TEXT,
"Result"  TEXT,
"Time"  TEXT NOT NULL,
"Username"  TEXT,
PRIMARY KEY ("Voice_id" ASC),
CONSTRAINT "FK_LiveRecognitionResults_Users_Username" FOREIGN KEY ("Username") REFERENCES "Users" ("Username") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Table structure for Secrets
-- ----------------------------
DROP TABLE IF EXISTS "main"."Secrets";
CREATE TABLE "Secrets" (
"ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"APPID"  TEXT,
"SECRET_ID"  TEXT,
"SECRET_KEY"  TEXT,
"UserName"  TEXT,
CONSTRAINT "FK_User" FOREIGN KEY ("UserName") REFERENCES "Users" ("Username") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Table structure for ShortRecognitionResults
-- ----------------------------
DROP TABLE IF EXISTS "main"."ShortRecognitionResults";
CREATE TABLE "ShortRecognitionResults" (
"RequestId"  TEXT NOT NULL,
"AudioDuration"  TEXT,
"Result"  TEXT,
"Time"  TEXT NOT NULL,
"Username"  TEXT,
PRIMARY KEY ("RequestId" ASC),
CONSTRAINT "FK_ShortRecognitionResults_Users_Username" FOREIGN KEY ("Username") REFERENCES "Users" ("Username") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Table structure for sqlite_sequence
-- ----------------------------
DROP TABLE IF EXISTS "main"."sqlite_sequence";
CREATE TABLE sqlite_sequence(name,seq);

-- ----------------------------
-- Table structure for Users
-- ----------------------------
DROP TABLE IF EXISTS "main"."Users";
CREATE TABLE "Users" (
    "Username" TEXT NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY,
    "Password" TEXT NOT NULL
);

-- ----------------------------
-- Table structure for _Secrets_old_20211211
-- ----------------------------
DROP TABLE IF EXISTS "main"."_Secrets_old_20211211";
CREATE TABLE "_Secrets_old_20211211" (
"ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"APPID"  TEXT,
"SECRET_ID"  TEXT,
"SECRET_KEY"  TEXT,
"UserName"  TEXT
);

-- ----------------------------
-- Indexes structure for table FlashRecognitionResults
-- ----------------------------
CREATE INDEX "main"."IX_FlashRecognitionResults_Username"
ON "FlashRecognitionResults" ("Username" ASC);

-- ----------------------------
-- Indexes structure for table LiveRecognitionResults
-- ----------------------------
CREATE INDEX "main"."IX_LiveRecognitionResults_Username"
ON "LiveRecognitionResults" ("Username" ASC);

-- ----------------------------
-- Indexes structure for table ShortRecognitionResults
-- ----------------------------
CREATE INDEX "main"."IX_ShortRecognitionResults_Username"
ON "ShortRecognitionResults" ("Username" ASC);

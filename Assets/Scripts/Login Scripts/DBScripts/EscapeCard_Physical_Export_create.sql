-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2022-09-03 05:04:09.05

-- process 1

-- tables
-- Table: account_log
CREATE TABLE account_log (
    account_log_id varchar(20) NOT NULL,
    account_login timestamp NULL DEFAULT CURRENT_TIMESTAMP,
    account_logout timestamp NULL,
    account_id varchar(20) NOT NULL,
    CONSTRAINT account_log_pk PRIMARY KEY (account_log_id)
);

-- Table: accounts
CREATE TABLE accounts (
    account_id varchar(20) NOT NULL,
    email varchar(50) NOT NULL,
    password varchar(100) NOT NULL,
    team_name varchar(50) NOT NULL,
    created_at timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    modified_at timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    region_id varchar(20) NOT NULL,
    CONSTRAINT accounts_pk PRIMARY KEY (account_id)
);

-- Table: cards
CREATE TABLE cards (
    card_id int NOT NULL,
    card_type varchar(15) NOT NULL,
    card_name varchar(20) NOT NULL,
    CONSTRAINT cards_pk PRIMARY KEY (card_id)
);

-- Table: members
CREATE TABLE members (
    member_id varchar(30) NOT NULL,
    name varchar(50) NOT NULL,
    member_type varchar(50) NOT NULL,
    created_at timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    modified_at timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
    account_id varchar(20) NOT NULL,
    CONSTRAINT members_pk PRIMARY KEY (member_id)
);

-- Table: players
CREATE TABLE players (
    player_id varchar(20) NOT NULL,
    remaining_coins int NOT NULL,
    remaining_hours time NOT NULL,
    discard_cards_count int NOT NULL,
    scores int NOT NULL,
    map_id int NOT NULL,
    account_id varchar(20) NOT NULL,
    CONSTRAINT players_pk PRIMARY KEY (player_id)
);

-- Table: players_cards
CREATE TABLE players_cards (
    player_id varchar(20) NOT NULL,
    card_id int NOT NULL,
    CONSTRAINT players_cards_pk PRIMARY KEY (player_id,card_id)
);

-- Table: regions
CREATE TABLE regions (
    region_id varchar(20) NOT NULL,
    region varchar(50) NOT NULL,
    CONSTRAINT regions_pk PRIMARY KEY (region_id)
);

-- foreign keys
-- Reference: account_log_accounts (table: account_log)
ALTER TABLE account_log ADD CONSTRAINT account_log_accounts FOREIGN KEY account_log_accounts (account_id)
    REFERENCES accounts (account_id);

-- Reference: accounts_regions (table: accounts)
ALTER TABLE accounts ADD CONSTRAINT accounts_regions FOREIGN KEY accounts_regions (region_id)
    REFERENCES regions (region_id);

-- Reference: members_accounts (table: members)
ALTER TABLE members ADD CONSTRAINT members_accounts FOREIGN KEY members_accounts (account_id)
    REFERENCES accounts (account_id);

-- Reference: players_accounts (table: players)
ALTER TABLE players ADD CONSTRAINT players_accounts FOREIGN KEY players_accounts (account_id)
    REFERENCES accounts (account_id);

-- Reference: players_cards_cards (table: players_cards)
ALTER TABLE players_cards ADD CONSTRAINT players_cards_cards FOREIGN KEY players_cards_cards (card_id)
    REFERENCES cards (card_id);

-- Reference: players_cards_players (table: players_cards)
ALTER TABLE players_cards ADD CONSTRAINT players_cards_players FOREIGN KEY players_cards_players (player_id)
    REFERENCES players (player_id);



CREATE TRIGGER `player_insert_Trigger` AFTER INSERT ON `accounts`
 FOR EACH ROW INSERT INTO `players` (`player_id`, `remaining_coins`, `remaining_hours`, `discard_cards_count`, `scores`, `map_id`, `account_id`) VALUES (uuid(), '0', '02:00:00', '0', '0', '0', new.account_id)


-- process 2

INSERT INTO `regions` (`region_id`, `region`) VALUES ('SBY', 'Surabaya');

-- insert queries separated so each uuid(in players) generated will be different
INSERT INTO `accounts` (`account_id`, `email`, `PASSWORD`, `team_name`, `created_at`, `modified_at`, `region_id`) VALUES 
('1', 'tesmail@gmail.com', 'ee54742a64ac6be74c68382ddca6d929', 'test1', current_timestamp(), NULL, 'SBY');

INSERT INTO `accounts` (`account_id`, `email`, `PASSWORD`, `team_name`, `created_at`, `modified_at`, `region_id`) VALUES 
('2', 'tesmail2@gmail.com', 'ee54742a64ac6be74c68382ddca6d929', 'test2', current_timestamp(), NULL, 'SBY');

INSERT INTO `accounts` (`account_id`, `email`, `PASSWORD`, `team_name`, `created_at`, `modified_at`, `region_id`) VALUES 
('3', 'tesmail3@gmail.com', 'ee54742a64ac6be74c68382ddca6d929', 'test3', current_timestamp(), NULL, 'SBY');


INSERT INTO `members` (`member_id`, `name`, `member_type`, `created_at`, `modified_at`, `account_id`) VALUES 
('1', 'Dante', 'Beban', current_timestamp(), NULL, '1'), 
('2', 'Sara', 'Beban', current_timestamp(), NULL, '1'), 
('3', 'Raiden', 'Beban', current_timestamp(), NULL, '2'); 

-- End of file.


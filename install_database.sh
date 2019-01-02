#!/usr/bin/env bash

source .config

mysqladmin -u$DB_USER -p$DB_PASSWORD -h 0.0.0.0  create $DB_NAME
mysql -u$DB_USER -p$DB_PASSWORD -h 0.0.0.0  $DB_NAME < database/database.sql

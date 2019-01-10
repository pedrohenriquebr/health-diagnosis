DEFAULT_STRUCTURE_SQL := structure.sql
SAMPLES_SQL := samples.sql

# MySQL Server
include .config

.PHONY : install create install-samples

define sql_exec
	mysql -u$(DB_USER) -p$(DB_PASSWORD) -h 0.0.0.0  $(DB_NAME) < $(1)
endef

create:
	@-mysqladmin -u$(DB_USER) -p$(DB_PASSWORD) -h 0.0.0.0  $@ $(DB_NAME)

install: create $(DEFAULT_STRUCTURE_SQL)
	@$(call sql_exec,$(DEFAULT_STRUCTURE_SQL))

install-samples: $(DEFAULT_STRUCTURE_SQL) $(SAMPLES_SQL)
	@$(call sql_exec,$(SAMPLES_SQL))

drop:
	@-echo "y"  | mysqladmin -u$(DB_USER) -p$(DB_PASSWORD) -h 0.0.0.0  $@ $(DB_NAME)
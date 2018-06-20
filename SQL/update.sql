ALTER TABLE [dbo].[summary-listings]
ALTER COLUMN neighbourhood nvarchar(40);

CREATE INDEX neighbourhood_index ON [dbo].[summary-listings] (neighbourhood)

CREATE INDEX price_index ON [dbo].[summary-listings] (price)

CREATE INDEX nr_of_reviews ON [dbo].[summary-listings] (number_of_reviews)



-- Security stuff

DELETE FROM [dbo].[neighbourhoods]

INSERT INTO       [dbo].[neighbourhoods] ( neighbourhood )
  SELECT DISTINCT neighbourhood
  FROM              [dbo].[listings]  
  WHERE neighbourhood IS NOT NULL

ALTER TABLE [dbo].[listings]
ALTER COLUMN neighbourhood nvarchar(40);

CREATE INDEX neighbourhood_index ON [dbo].[listings] (neighbourhood)
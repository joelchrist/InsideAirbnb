DELETE FROM [dbo].[neighbourhoods]

INSERT INTO       [dbo].[neighbourhoods] ( neighbourhood )
  SELECT DISTINCT neighbourhood
  FROM              [dbo].[listings]  
  WHERE neighbourhood IS NOT NULL
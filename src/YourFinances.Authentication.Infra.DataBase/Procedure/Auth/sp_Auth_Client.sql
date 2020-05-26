CREATE PROCEDURE [dbo].[sp_Auth_Client]
	@ClientId varchar(150),
	@ClientSecrete varchar(150)
AS
BEGIN
	
	SELECT 
		Id,
		ClientId,
		ClientSecret,
		Active  
	FROM 
		Client 
	WHERE 
		ClientId =@ClientId 
	AND 
		ClientSecret=@ClientSecrete and Active =1

	RETURN 
END

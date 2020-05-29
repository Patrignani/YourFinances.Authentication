CREATE PROCEDURE [dbo].[Auth_Client_sp]
	@ClientId varchar(150),
	@ClientSecrete varchar(150)
AS
BEGIN
	
	SELECT 
		Id,
		ClientId,
		ClientSecret,
		Identification,
		Active  
	FROM 
		Client 
	WHERE 
		ClientId =@ClientId 
	AND 
		ClientSecret=@ClientSecrete and Active =1

	RETURN 
END

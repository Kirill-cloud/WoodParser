CREATE TABLE [dbo].[Deal]
(
	[DealNumber] NCHAR(50) NOT NULL PRIMARY KEY, 
    [Buyer] INT NOT NULL, 
    [Seller] INT NOT NULL, 
    [BuyerVolume] DECIMAL NULL, 
    [SellerVolume] DECIMAL NULL, 
    [Date] DATETIME NOT NULL, 
    CONSTRAINT [FK_DealBuyer_ToSubjects] FOREIGN KEY (Buyer) REFERENCES [Subject]([id]),
    CONSTRAINT [FK_DealSeller_ToSubjects] FOREIGN KEY (Seller) REFERENCES [Subject]([id]),
)

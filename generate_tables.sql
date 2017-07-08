CREATE TABLE [dbo].[Order_Items] (
    [order_items_id]         INT     NOT NULL,
    [product_id]             INT     NOT NULL,
    [order_id]               INT     NOT NULL,
    [order_item_status_code] TINYINT NOT NULL,
    [order_item_quantity]    TINYINT NOT NULL,
    [order_item_price]       MONEY   NOT NULL,
 );

 CREATE TABLE [dbo].[Payments] (
    [payment_id]     INT   NOT NULL,
    [invoice_number] INT   NOT NULL,
    [payment_date]   DATE  NOT NULL,
    [payment_amount] MONEY NOT NULL,
);

CREATE TABLE [dbo].[Product_Image] (
    [image_id]   INT           NOT NULL,
    [product_id] INT           NOT NULL,
    [image_url]  NVARCHAR (50) NOT NULL,
);

CREATE TABLE [dbo].[Products] (
    [product_id]          INT            NOT NULL,
    [product_type_code]   TINYINT        NOT NULL,
    [product_name]        NVARCHAR (50)  NOT NULL,
    [product_price]       MONEY          NOT NULL,
    [product_size]        NCHAR (10)     NOT NULL,
    [product_color]       NVARCHAR (50)  NOT NULL,
    [special_offer]       NVARCHAR (50)  NULL,
    [product_description] NVARCHAR (MAX) NOT NULL,
 );

 CREATE TABLE [dbo].[Ref_Invoice_Status_Codes] (
    [invoice_status_code]        TINYINT    NOT NULL,
    [invoice_status_description] NCHAR (10) NOT NULL,
);

CREATE TABLE [dbo].[Ref_Order_Item_Status_Codes] (
    [order_item_status_code]        TINYINT       NOT NULL,
    [order_item_status_description] NVARCHAR (50) NOT NULL,
 );

 CREATE TABLE [dbo].[Ref_Order_Status_Codes] (
    [order_status_code]        TINYINT    NOT NULL,
    [order_status_description] NCHAR (10) NOT NULL,
);

CREATE TABLE [dbo].[Ref_Payment_Methods] (
    [payment_method_code]        TINYINT        NOT NULL,
    [payment_method_description] NVARCHAR (MAX) NOT NULL,
);

CREATE TABLE [dbo].[Ref_Product_Type] (
    [product_type_code]        TINYINT       NOT NULL,
    [product_parent_type_code] TINYINT       NULL,
    [product_type_description] NVARCHAR (50) NOT NULL,
);

CREATE TABLE [dbo].[Shipment_Items] (
    [shipment_id]    INT NOT NULL,
    [order_items_id] INT NOT NULL,
);

CREATE TABLE [dbo].[Shipments] (
    [shipment_id]              INT            NOT NULL,
    [order_id]                 INT            NOT NULL,
    [invoice_number]           INT            NOT NULL,
    [shipment_tracking_number] NVARCHAR (50)  NOT NULL,
    [shipment_date]            DATE           NOT NULL,
    [other_shipment_details]   NVARCHAR (MAX) NULL,
);


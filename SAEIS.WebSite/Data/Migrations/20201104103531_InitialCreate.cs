using Microsoft.EntityFrameworkCore.Migrations;

namespace SAEIS.WebSite.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.CreateTable(
                name: "BioRegion",
                columns: table => new
                {
                    BioRegionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BioRegion", x => x.BioRegionID);
                });

            migrationBuilder.CreateTable(
                name: "Classification",
                columns: table => new
                {
                    ClassificationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassificationType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.ClassificationID);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    ConditionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionType = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    LongDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.ConditionID);
                });

            migrationBuilder.CreateTable(
                name: "Dataset",
                columns: table => new
                {
                    DatasetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dataset", x => x.DatasetID);
                });

            migrationBuilder.CreateTable(
                name: "Geomorphology",
                columns: table => new
                {
                    GeomorphologyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geomorphology", x => x.GeomorphologyID);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    SubType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Available = table.Column<string>(nullable: true),
                    ImageDate = table.Column<string>(nullable: true),
                    LinkToImage = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "InfoAvailable",
                columns: table => new
                {
                    InfoAvailableID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoAvailable", x => x.InfoAvailableID);
                });

            migrationBuilder.CreateTable(
                name: "IssueType",
                columns: table => new
                {
                    IssueTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueType", x => x.IssueTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Literature",
                columns: table => new
                {
                    LiteratureID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    SubType = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    PublishDate = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Available = table.Column<string>(nullable: true),
                    LinkToManuscript = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Literature", x => x.LiteratureID);
                });

            migrationBuilder.CreateTable(
                name: "ManagementClassification",
                columns: table => new
                {
                    ManagementClassificationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassificationType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementClassification", x => x.ManagementClassificationID);
                });

            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    MapID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    SubType = table.Column<string>(nullable: true),
                    MapName = table.Column<string>(nullable: true),
                    Available = table.Column<string>(nullable: true),
                    LinkToMap = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.MapID);
                });

            migrationBuilder.CreateTable(
                name: "PriorityForRehabilitation",
                columns: table => new
                {
                    PriorityForRehabilitationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityForRehabilitation", x => x.PriorityForRehabilitationID);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceID);
                });

            migrationBuilder.CreateTable(
                name: "SancutaryProtection",
                columns: table => new
                {
                    SancutaryProtectionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecommendedExtent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SancutaryProtection", x => x.SancutaryProtectionID);
                });

            migrationBuilder.CreateTable(
                name: "UndevelopedMargin",
                columns: table => new
                {
                    UndevelopedMarginID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecommendedExtent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UndevelopedMargin", x => x.UndevelopedMarginID);
                });

            migrationBuilder.CreateTable(
                name: "WaterQuality",
                columns: table => new
                {
                    WaterQualityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterQuality", x => x.WaterQualityID);
                });

            migrationBuilder.CreateTable(
                name: "WaterRequirement",
                columns: table => new
                {
                    WaterRequirementID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecommendedMinimum = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterRequirement", x => x.WaterRequirementID);
                });

            migrationBuilder.CreateTable(
                name: "DatasetVariable",
                columns: table => new
                {
                    DatasetVariableID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    SubType = table.Column<string>(nullable: true),
                    VariableType = table.Column<string>(nullable: true),
                    VariableName = table.Column<string>(nullable: true),
                    LinkToDocument = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatasetVariable", x => x.DatasetVariableID);
                    table.ForeignKey(
                        name: "FK_DatasetVariable_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "DatasetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estuary",
                columns: table => new
                {
                    EstuaryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstuaryName = table.Column<string>(nullable: true),
                    AlternativeName = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    ClassificationId = table.Column<int>(nullable: true),
                    ClassificationDate = table.Column<string>(nullable: true),
                    ConditionId = table.Column<int>(nullable: true),
                    ConditionDate = table.Column<string>(nullable: true),
                    ConditionRemarks = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    BioRegionID = table.Column<int>(nullable: true),
                    GeomorphologyId = table.Column<int>(nullable: true),
                    InfoAvailableId = table.Column<int>(nullable: true),
                    WaterQualityId = table.Column<int>(nullable: true),
                    ManagementClassificationId = table.Column<int>(nullable: true),
                    SancutaryProtectionID = table.Column<int>(nullable: true),
                    UndevelopedMarginId = table.Column<int>(nullable: true),
                    WaterRequirementId = table.Column<int>(nullable: true),
                    PriorityForRehabilitationId = table.Column<int>(nullable: true),
                    LatitudeDecimalDegrees = table.Column<decimal>(nullable: true),
                    LongitudeDecimalDegrees = table.Column<decimal>(nullable: true),
                    MouthOpen = table.Column<int>(nullable: true),
                    AreaFloodplain = table.Column<decimal>(nullable: true),
                    AreaWater = table.Column<decimal>(nullable: true),
                    LengthShoreLine = table.Column<decimal>(nullable: true),
                    LengthRiver = table.Column<int>(nullable: true),
                    AreaCatchment = table.Column<int>(nullable: true),
                    AreaCatchmentDEAT = table.Column<int>(nullable: true),
                    MARunnoff = table.Column<int>(nullable: true),
                    MinFlowReq = table.Column<int>(nullable: true),
                    PercentAgriculture = table.Column<int>(nullable: true),
                    PercentDegraded = table.Column<int>(nullable: true),
                    PercentNatural = table.Column<int>(nullable: true),
                    PecentUrban = table.Column<int>(nullable: true),
                    LandCoverReference = table.Column<string>(nullable: true),
                    SizeConsImp = table.Column<int>(nullable: true),
                    ZonalRarityConsImp = table.Column<int>(nullable: true),
                    HabitatConsImp = table.Column<int>(nullable: true),
                    BiodiversityConsImp = table.Column<decimal>(nullable: true),
                    ScoreConsImp = table.Column<decimal>(nullable: true),
                    RankConsImp = table.Column<int>(nullable: true),
                    DemandScoreRDM = table.Column<int>(nullable: true),
                    ImpScoreRDM = table.Column<decimal>(nullable: true),
                    PriorityRDM = table.Column<int>(nullable: true),
                    DemandCatRDM = table.Column<string>(nullable: true),
                    ImpCatRDM = table.Column<string>(nullable: true),
                    ProvincialConsImp = table.Column<string>(nullable: true),
                    NationalConsImp = table.Column<string>(nullable: true),
                    BiblioWitfieldLink = table.Column<string>(nullable: true),
                    WaterQualityPollution = table.Column<string>(nullable: true),
                    WaterQualitySilt = table.Column<string>(nullable: true),
                    WaterQuantityGen = table.Column<string>(nullable: true),
                    AlienClearing = table.Column<string>(nullable: true),
                    FixAppropriateBankStabilisation = table.Column<string>(nullable: true),
                    MouthManagement = table.Column<string>(nullable: true),
                    RehabilitationComments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estuary", x => x.EstuaryID);
                    table.ForeignKey(
                        name: "FK_Estuary_Classification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classification",
                        principalColumn: "ClassificationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_Condition_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Condition",
                        principalColumn: "ConditionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_Geomorphology_GeomorphologyId",
                        column: x => x.GeomorphologyId,
                        principalTable: "Geomorphology",
                        principalColumn: "GeomorphologyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_InfoAvailable_InfoAvailableId",
                        column: x => x.InfoAvailableId,
                        principalTable: "InfoAvailable",
                        principalColumn: "InfoAvailableID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_ManagementClassification_ManagementClassificationId",
                        column: x => x.ManagementClassificationId,
                        principalTable: "ManagementClassification",
                        principalColumn: "ManagementClassificationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_PriorityForRehabilitation_PriorityForRehabilitationId",
                        column: x => x.PriorityForRehabilitationId,
                        principalTable: "PriorityForRehabilitation",
                        principalColumn: "PriorityForRehabilitationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_BioRegion_BioRegionID",
                        column: x => x.BioRegionID,
                        principalTable: "BioRegion",
                        principalColumn: "BioRegionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_SancutaryProtection_SancutaryProtectionID",
                        column: x => x.SancutaryProtectionID,
                        principalTable: "SancutaryProtection",
                        principalColumn: "SancutaryProtectionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_UndevelopedMargin_UndevelopedMarginId",
                        column: x => x.UndevelopedMarginId,
                        principalTable: "UndevelopedMargin",
                        principalColumn: "UndevelopedMarginID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_WaterQuality_WaterQualityId",
                        column: x => x.WaterQualityId,
                        principalTable: "WaterQuality",
                        principalColumn: "WaterQualityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estuary_WaterRequirement_WaterRequirementId",
                        column: x => x.WaterRequirementId,
                        principalTable: "WaterRequirement",
                        principalColumn: "WaterRequirementID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstuaryDataset",
                columns: table => new
                {
                    EstuaryId = table.Column<int>(nullable: false),
                    DatasetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstuaryDataset", x => new { x.EstuaryId, x.DatasetId });
                    table.ForeignKey(
                        name: "FK_EstuaryDataset_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "DatasetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstuaryDataset_Estuary_EstuaryId",
                        column: x => x.EstuaryId,
                        principalTable: "Estuary",
                        principalColumn: "EstuaryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstuaryImage",
                columns: table => new
                {
                    EstuaryId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstuaryImage", x => new { x.EstuaryId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_EstuaryImage_Estuary_EstuaryId",
                        column: x => x.EstuaryId,
                        principalTable: "Estuary",
                        principalColumn: "EstuaryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstuaryImage_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstuaryLiterature",
                columns: table => new
                {
                    EstuaryId = table.Column<int>(nullable: false),
                    LiteratureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstuaryLiterature", x => new { x.EstuaryId, x.LiteratureId });
                    table.ForeignKey(
                        name: "FK_EstuaryLiterature_Estuary_EstuaryId",
                        column: x => x.EstuaryId,
                        principalTable: "Estuary",
                        principalColumn: "EstuaryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstuaryLiterature_Literature_LiteratureId",
                        column: x => x.LiteratureId,
                        principalTable: "Literature",
                        principalColumn: "LiteratureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstuaryMap",
                columns: table => new
                {
                    EstuaryId = table.Column<int>(nullable: false),
                    MapId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstuaryMap", x => new { x.EstuaryId, x.MapId });
                    table.ForeignKey(
                        name: "FK_EstuaryMap_Estuary_EstuaryId",
                        column: x => x.EstuaryId,
                        principalTable: "Estuary",
                        principalColumn: "EstuaryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstuaryMap_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "MapID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    IssueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstuaryId = table.Column<int>(nullable: false),
                    IssueTypeId = table.Column<int>(nullable: false),
                    IssueHeader = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Responses = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.IssueID);
                    table.ForeignKey(
                        name: "FK_Issue_Estuary_EstuaryId",
                        column: x => x.EstuaryId,
                        principalTable: "Estuary",
                        principalColumn: "EstuaryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_IssueType_IssueTypeId",
                        column: x => x.IssueTypeId,
                        principalTable: "IssueType",
                        principalColumn: "IssueTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatasetVariable_DatasetId",
                table: "DatasetVariable",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_ClassificationId",
                table: "Estuary",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_ConditionId",
                table: "Estuary",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_GeomorphologyId",
                table: "Estuary",
                column: "GeomorphologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_InfoAvailableId",
                table: "Estuary",
                column: "InfoAvailableId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_ManagementClassificationId",
                table: "Estuary",
                column: "ManagementClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_PriorityForRehabilitationId",
                table: "Estuary",
                column: "PriorityForRehabilitationId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_ProvinceId",
                table: "Estuary",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_BioRegionID",
                table: "Estuary",
                column: "BioRegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_SancutaryProtectionID",
                table: "Estuary",
                column: "SancutaryProtectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_UndevelopedMarginId",
                table: "Estuary",
                column: "UndevelopedMarginId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_WaterQualityId",
                table: "Estuary",
                column: "WaterQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_Estuary_WaterRequirementId",
                table: "Estuary",
                column: "WaterRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_EstuaryDataset_DatasetId",
                table: "EstuaryDataset",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_EstuaryImage_ImageId",
                table: "EstuaryImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_EstuaryLiterature_LiteratureId",
                table: "EstuaryLiterature",
                column: "LiteratureId");

            migrationBuilder.CreateIndex(
                name: "IX_EstuaryMap_MapId",
                table: "EstuaryMap",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_EstuaryId",
                table: "Issue",
                column: "EstuaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_IssueTypeId",
                table: "Issue",
                column: "IssueTypeId");
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropTable(
                name: "DatasetVariable");

            migrationBuilder.DropTable(
                name: "EstuaryDataset");

            migrationBuilder.DropTable(
                name: "EstuaryImage");

            migrationBuilder.DropTable(
                name: "EstuaryLiterature");

            migrationBuilder.DropTable(
                name: "EstuaryMap");

            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "Dataset");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Literature");

            migrationBuilder.DropTable(
                name: "Map");

            migrationBuilder.DropTable(
                name: "Estuary");

            migrationBuilder.DropTable(
                name: "IssueType");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "Geomorphology");

            migrationBuilder.DropTable(
                name: "InfoAvailable");

            migrationBuilder.DropTable(
                name: "ManagementClassification");

            migrationBuilder.DropTable(
                name: "PriorityForRehabilitation");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "BioRegion");

            migrationBuilder.DropTable(
                name: "SancutaryProtection");

            migrationBuilder.DropTable(
                name: "UndevelopedMargin");

            migrationBuilder.DropTable(
                name: "WaterQuality");

            migrationBuilder.DropTable(
                name: "WaterRequirement");
            */
        }
    }
}

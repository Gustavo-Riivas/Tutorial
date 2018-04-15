protected override void Up(MigrationBuilder migrationBuilder)
{
    // (... some code)

    migrationBuilder.CreateTable(
        name: "Items",
        columns: table => new
        {
            Id = table.Column<Guid>(type: "BLOB", nullable: false),
            DueAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
            IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
            Title = table.Column<string>(type: "TEXT", nullable: true)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Items", x => x.Id);
        });

    // (some code...)
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    // (... some code)

    migrationBuilder.DropTable(
        name: "Items");

    // (some code...)
}
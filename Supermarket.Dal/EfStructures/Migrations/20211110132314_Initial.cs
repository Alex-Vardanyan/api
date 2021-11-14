using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Supermarket.Dal.EfStructures.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address_location",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    district = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    street = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    apartment = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    building_number = table.Column<int>(type: "int", nullable: true),
                    postal_code = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address_location", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "delivery_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "proffesion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prof_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proffesion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    username = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: true),
                    passwd = table.Column<string>(type: "char(64)", unicode: false, fixedLength: true, maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "branch",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    location_id = table.Column<int>(type: "int", nullable: true),
                    frezer_volume = table.Column<int>(type: "int", nullable: true),
                    storage_volume = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branch", x => x.id);
                    table.ForeignKey(
                        name: "FK__branch__location__70DDC3D8",
                        column: x => x.location_id,
                        principalTable: "address_location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    location_id = table.Column<int>(type: "int", nullable: true),
                    contract_exp_date = table.Column<DateTime>(type: "date", nullable: true),
                    contact_num = table.Column<int>(type: "int", nullable: true),
                    contact_email = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.id);
                    table.ForeignKey(
                        name: "FK__supplier__locati__160F4887",
                        column: x => x.location_id,
                        principalTable: "address_location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_id = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                    table.ForeignKey(
                        name: "FK__category__depart__74AE54BC",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "open_positions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    position_id = table.Column<int>(type: "int", nullable: true),
                    descrription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_open_positions", x => x.id);
                    table.ForeignKey(
                        name: "FK__open_posi__posit__06CD04F7",
                        column: x => x.position_id,
                        principalTable: "proffesion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    birth_date = table.Column<DateTime>(type: "date", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phone_number = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: true),
                    address_id = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__B9BE370FDA7C730B", x => x.user_id);
                    table.ForeignKey(
                        name: "FK__customer__addres__797309D9",
                        column: x => x.address_id,
                        principalTable: "address_location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__customer__user_i__787EE5A0",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "deliveryman",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    car_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    car_number = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    car_model = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__delivery__C52E0BA83EB0395A", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK__deliverym__emplo__7A672E12",
                        column: x => x.employee_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    priority = table.Column<int>(type: "int", nullable: true),
                    employee_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.id);
                    table.ForeignKey(
                        name: "FK__jobs__employee_i__02084FDA",
                        column: x => x.employee_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "log_sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    log_in = table.Column<DateTime>(type: "datetime", nullable: true),
                    log_out = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK__log_sessi__user___02FC7413",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cashbox",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branch_id = table.Column<int>(type: "int", nullable: true),
                    money = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cashbox", x => x.id);
                    table.ForeignKey(
                        name: "FK__cashbox__branch___71D1E811",
                        column: x => x.branch_id,
                        principalTable: "branch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    users_id = table.Column<int>(type: "int", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    birth_date = table.Column<DateTime>(type: "date", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    phone_number = table.Column<string>(type: "char(12)", unicode: false, fixedLength: true, maxLength: 12, nullable: true),
                    address_id = table.Column<int>(type: "int", nullable: true),
                    profession_id = table.Column<int>(type: "int", nullable: true),
                    starting_salary = table.Column<decimal>(type: "money", nullable: true),
                    salary = table.Column<decimal>(type: "money", nullable: true),
                    firstday_date = table.Column<DateTime>(type: "date", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    department_id = table.Column<int>(type: "int", nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.id);
                    table.ForeignKey(
                        name: "FK__employee__addres__7E37BEF6",
                        column: x => x.address_id,
                        principalTable: "address_location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__employee__branch__01142BA1",
                        column: x => x.branch_id,
                        principalTable: "branch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__employee__depart__00200768",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__employee__profes__7F2BE32F",
                        column: x => x.profession_id,
                        principalTable: "proffesion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__employee__users___7D439ABD",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    delivery_status_id = table.Column<int>(type: "int", nullable: true),
                    order_status_id = table.Column<int>(type: "int", nullable: true),
                    delivery_man_id = table.Column<int>(type: "int", nullable: true),
                    order_description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: true),
                    peyment_status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    delivered = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK__order__branch_id__0B91BA14",
                        column: x => x.branch_id,
                        principalTable: "branch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__customer___07C12930",
                        column: x => x.customer_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__delivery___08B54D69",
                        column: x => x.delivery_status_id,
                        principalTable: "delivery_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__delivery___0A9D95DB",
                        column: x => x.delivery_man_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__order_sta__09A971A2",
                        column: x => x.order_status_id,
                        principalTable: "order_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: true),
                    cost = table.Column<decimal>(type: "money", nullable: true),
                    code = table.Column<int>(type: "int", nullable: true),
                    volume = table.Column<int>(type: "int", nullable: true),
                    refunded = table.Column<bool>(type: "bit", nullable: true),
                    supplier_id = table.Column<int>(type: "int", nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK__product__categor__0F624AF8",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__product__supplie__0E6E26BF",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "checkAndDispose_job",
                columns: table => new
                {
                    jobs_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__checkAnd__2696017DBF4B6AA8", x => x.jobs_id);
                    table.ForeignKey(
                        name: "FK__checkAndD__jobs___778AC167",
                        column: x => x.jobs_id,
                        principalTable: "jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cashbox_transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cashier = table.Column<int>(type: "int", nullable: true),
                    cashbox_id = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cashbox_transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK__cashbox_t__cashb__73BA3083",
                        column: x => x.cashbox_id,
                        principalTable: "cashbox",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__cashbox_t__cashi__72C60C4A",
                        column: x => x.cashier,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cell_products",
                columns: table => new
                {
                    branch_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    dep_quantity = table.Column<int>(type: "int", nullable: true),
                    optimal_quantity = table.Column<int>(type: "int", nullable: true),
                    max_quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cell_pro__A12E100189F40DA8", x => new { x.branch_id, x.product_id });
                    table.ForeignKey(
                        name: "FK__cell_prod__branc__75A278F5",
                        column: x => x.branch_id,
                        principalTable: "branch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__cell_prod__produ__76969D2E",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dispose_package",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    volume = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispose_package", x => x.id);
                    table.ForeignKey(
                        name: "FK__dispose_p__branc__7C4F7684",
                        column: x => x.branch_id,
                        principalTable: "branch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__dispose_p__produ__7B5B524B",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "logistics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    destination_branch_id = table.Column<int>(type: "int", nullable: true),
                    starting_branch_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sent_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    arrived_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logistics", x => x.id);
                    table.ForeignKey(
                        name: "FK__logistics__desti__03F0984C",
                        column: x => x.destination_branch_id,
                        principalTable: "branch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__logistics__produ__05D8E0BE",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__logistics__start__04E4BC85",
                        column: x => x.starting_branch_id,
                        principalTable: "branch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_product",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_pr__022945F643A30D72", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "FK__order_pro__order__0C85DE4D",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order_pro__produ__0D7A0286",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_package",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branch_id = table.Column<int>(type: "int", nullable: true),
                    prod_id = table.Column<int>(type: "int", nullable: true),
                    warehouse_quantity = table.Column<int>(type: "int", nullable: true),
                    dep_quantity = table.Column<int>(type: "int", nullable: true),
                    expiration_date = table.Column<DateTime>(type: "date", nullable: true),
                    volume = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_package", x => x.id);
                    table.ForeignKey(
                        name: "FK__product_p__branc__10566F31",
                        column: x => x.branch_id,
                        principalTable: "branch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__product_p__prod___114A936A",
                        column: x => x.prod_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shipping",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    destination_branch_id = table.Column<int>(type: "int", nullable: true),
                    supplier_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    sent_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    arrived_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping", x => x.id);
                    table.ForeignKey(
                        name: "FK__shipping__destin__1332DBDC",
                        column: x => x.destination_branch_id,
                        principalTable: "branch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__shipping__produc__123EB7A3",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__shipping__suppli__14270015",
                        column: x => x.supplier_id,
                        principalTable: "supplier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "special_care",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    max_temp = table.Column<int>(type: "int", nullable: true),
                    min_temp = table.Column<int>(type: "int", nullable: true),
                    expiration_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__special___47027DF575F7D6EE", x => x.product_id);
                    table.ForeignKey(
                        name: "FK__special_c__produ__151B244E",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "warehouseToDepartment_jobs",
                columns: table => new
                {
                    jobs_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__warehous__2696017DDED06737", x => x.jobs_id);
                    table.ForeignKey(
                        name: "FK__warehouse__jobs___1BC821DD",
                        column: x => x.jobs_id,
                        principalTable: "jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__warehouse__produ__1CBC4616",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "wish_list",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__wish_lis__0BD4214D53CE60A9", x => new { x.product_id, x.customer_id });
                    table.ForeignKey(
                        name: "FK__wish_list__custo__1EA48E88",
                        column: x => x.customer_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__wish_list__produ__1DB06A4F",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "transaction_product",
                columns: table => new
                {
                    transaction_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__transact__C1B62770E1A34510", x => new { x.transaction_id, x.product_id });
                    table.ForeignKey(
                        name: "FK__transacti__produ__17F790F9",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__transacti__trans__17036CC0",
                        column: x => x.transaction_id,
                        principalTable: "cashbox_transaction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "warehouse_jobs",
                columns: table => new
                {
                    jobs_id = table.Column<int>(type: "int", nullable: false),
                    shipping_id = table.Column<int>(type: "int", nullable: true),
                    logistics_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__warehous__2696017D04CBC392", x => x.jobs_id);
                    table.ForeignKey(
                        name: "FK__warehouse__jobs___18EBB532",
                        column: x => x.jobs_id,
                        principalTable: "jobs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__warehouse__logis__1AD3FDA4",
                        column: x => x.logistics_id,
                        principalTable: "logistics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__warehouse__shipp__19DFD96B",
                        column: x => x.shipping_id,
                        principalTable: "shipping",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_branch_location_id",
                table: "branch",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_cashbox_branch_id",
                table: "cashbox",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_cashbox_transaction_cashbox_id",
                table: "cashbox_transaction",
                column: "cashbox_id");

            migrationBuilder.CreateIndex(
                name: "IX_cashbox_transaction_cashier",
                table: "cashbox_transaction",
                column: "cashier");

            migrationBuilder.CreateIndex(
                name: "IX_category_department_id",
                table: "category",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_cell_products_product_id",
                table: "cell_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_address_id",
                table: "customer",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_dispose_package_branch_id",
                table: "dispose_package",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_dispose_package_product_id",
                table: "dispose_package",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_address_id",
                table: "employee",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_branch_id",
                table: "employee",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_department_id",
                table: "employee",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_profession_id",
                table: "employee",
                column: "profession_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_users_id",
                table: "employee",
                column: "users_id");

            migrationBuilder.CreateIndex(
                name: "IX_jobs_employee_id",
                table: "jobs",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_log_sessions_user_id",
                table: "log_sessions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_logistics_destination_branch_id",
                table: "logistics",
                column: "destination_branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_logistics_product_id",
                table: "logistics",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_logistics_starting_branch_id",
                table: "logistics",
                column: "starting_branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_open_positions_position_id",
                table: "open_positions",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_branch_id",
                table: "order",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_customer_id",
                table: "order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_delivery_man_id",
                table: "order",
                column: "delivery_man_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_delivery_status_id",
                table: "order",
                column: "delivery_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_order_status_id",
                table: "order",
                column: "order_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_product_product_id",
                table: "order_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_supplier_id",
                table: "product",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_package_branch_id",
                table: "product_package",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_package_prod_id",
                table: "product_package",
                column: "prod_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_destination_branch_id",
                table: "shipping",
                column: "destination_branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_product_id",
                table: "shipping",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_supplier_id",
                table: "shipping",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_supplier_location_id",
                table: "supplier",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_product_product_id",
                table: "transaction_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_jobs_logistics_id",
                table: "warehouse_jobs",
                column: "logistics_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouse_jobs_shipping_id",
                table: "warehouse_jobs",
                column: "shipping_id");

            migrationBuilder.CreateIndex(
                name: "IX_warehouseToDepartment_jobs_product_id",
                table: "warehouseToDepartment_jobs",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_wish_list_customer_id",
                table: "wish_list",
                column: "customer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cell_products");

            migrationBuilder.DropTable(
                name: "checkAndDispose_job");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "deliveryman");

            migrationBuilder.DropTable(
                name: "dispose_package");

            migrationBuilder.DropTable(
                name: "log_sessions");

            migrationBuilder.DropTable(
                name: "open_positions");

            migrationBuilder.DropTable(
                name: "order_product");

            migrationBuilder.DropTable(
                name: "product_package");

            migrationBuilder.DropTable(
                name: "special_care");

            migrationBuilder.DropTable(
                name: "transaction_product");

            migrationBuilder.DropTable(
                name: "warehouse_jobs");

            migrationBuilder.DropTable(
                name: "warehouseToDepartment_jobs");

            migrationBuilder.DropTable(
                name: "wish_list");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "cashbox_transaction");

            migrationBuilder.DropTable(
                name: "logistics");

            migrationBuilder.DropTable(
                name: "shipping");

            migrationBuilder.DropTable(
                name: "jobs");

            migrationBuilder.DropTable(
                name: "delivery_status");

            migrationBuilder.DropTable(
                name: "order_status");

            migrationBuilder.DropTable(
                name: "cashbox");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "branch");

            migrationBuilder.DropTable(
                name: "proffesion");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "address_location");
        }
    }
}

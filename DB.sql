CREATE TABLE cuentas (
	ID int(11) PRIMARY KEY NOT NULL,
	Name varchar(24) NOT NULL DEFAULT 'none',
	Password varchar(65) NOT NULL DEFAULT 'none',
	Email varchar(256) NOT NULL DEFAULT '-',
	IP varchar(18) NOT NULL DEFAULT '127.0.0.1',
);

CREATE TABLE personajes (
	ID int(11) PRIMARY KEY NOT NULL,
	Nombre_Apellido varchar(24) NOT NULL DEFAULT 'none',
	CuentaID int(11) NOT NULL DEFAULT '-1',
	PosicionX float NOT NULL DEFAULT '0',
	PosicionY float NOT NULL DEFAULT '0',
	PosicionZ float NOT NULL DEFAULT '0',
	PosicionR float NOT NULL DEFAULT '0',
	Interior int(11) NOT NULL DEFAULT '0',
	VirtualWorld int(11) NOT NULL DEFAULT '0',
	Vida float NOT NULL DEFAULT '100.0',
	Chaleco float NOT NULL DEFAULT '0',
	Skin int(11) NOT NULL DEFAULT '289',
);

CREATE TABlE inventario (
	ID int(11) PRIMARY KEY NOT NULL AUTO_INCREMENT,
	IDCharacter int (11) NOT NULL,
	Slot1 int(11) NOT NULL DEFAULT '0',
	Slot2 int(11) NOT NULL DEFAULT '0',
	Slot3 int(11) NOT NULL DEFAULT '0',
	Slot4 int(11) NOT NULL DEFAULT '0',
	Slot5 int(11) NOT NULL DEFAULT '0'
);

CREATE TABlE Belts (
	ID int(11) PRIMARY KEY NOT NULL AUTO_INCREMENT,
	Character int (11) NOT NULL,
	Slot1 int(11) NOT NULL DEFAULT '0',
	SlotAmount1 int(11) NOT NULL DEFAULT '0',
	Slot2 int(11) NOT NULL DEFAULT '0',
	SlotAmount2 int(11) NOT NULL DEFAULT '0',
	Slot3 int(11) NOT NULL DEFAULT '0',
	SlotAmount3 int(11) NOT NULL DEFAULT '0'
);

ALTER TABLE personajes
	ADD Crack int(11) NOT NULL DEFAULT '0';

ALTER TABLE personajes
	ADD RightHand int(11) NOT NULL DEFAULT '0';

ALTER TABLE personajes
	ADD LeftHand int(11) NOT NULL DEFAULT '0';

ALTER TABLE inventario
	ADD SlotAmount1 int(11) NOT NULL DEFAULT '0' AFTER Slot1;

ALTER TABLE inventario
	ADD SlotAmount2 int(11) NOT NULL DEFAULT '0' AFTER Slot2;

ALTER TABLE inventario
	ADD SlotAmount3 int(11) NOT NULL DEFAULT '0' AFTER Slot3;

ALTER TABLE inventario
	ADD SlotAmount4 int(11) NOT NULL DEFAULT '0' AFTER Slot4;

ALTER TABLE inventario
	ADD SlotAmount5 int(11) NOT NULL DEFAULT '0' AFTER Slot5;

ALTER TABLE personajes
	ADD RightHandAmount int(11) NOT NULL DEFAULT '0' AFTER RightHand;
ALTER TABLE personajes
	ADD LeftHandAmount int(11) NOT NULL DEFAULT '0' AFTER LeftHand;
ALTER TABLE personajes
	ADD RightDoll int(11) NOT NULL DEFAULT '0';
ALTER TABLE personajes
	ADD RightDollAmount int(11) NOT NULL DEFAULT '0';
ALTER TABLE personajes
	ADD LeftDoll int(11) NOT NULL DEFAULT '0';
ALTER TABLE personajes
	ADD LeftDollAmount int(11) NOT NULL DEFAULT '0';

ALTER TABLE cuentas
	MODIFY ID int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE personajes
	MODIFY ID int(11) NOT NULL AUTO_INCREMENT;
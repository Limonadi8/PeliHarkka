var barrel, bullets, velocity = 1200, nextFire = 0, fireRate = 500, enemy, bullet, enemyGroup, money = 50,m,b2,auto = false;
var style = { font: "65px Arial", fill: "#000000", align: "center" };

demo.state2 = function(){};
demo.state2.prototype = {
  preload: function(){
    game.load.image('base', 'assets/sprites/cannonBase.png');
    game.load.image('barrel', 'assets/sprites/cannonBarrel.png');
    game.load.image('bullet', 'assets/sprites/bullet.png');
    game.load.image('button1', 'assets/sprites/button1.png');
    game.load.image('button2', 'assets/sprites/button2.png');
    game.load.audio('pops', 'assets/sounds/buttonPops.mp3');
    game.load.image('tree', 'assets/backgrounds/treeBG.png');
  },
  create: function(){
    var treeBG = game.add.sprite(0, 0, 'tree');
    sound = game.add.audio('pops');
    sound.addMarker('low', 0.15, 0.5);
    sound.addMarker('high', 1.1, 1.5);
    var t = game.add.text(game.world.centerX-600, 0, "tulinopeus: " + fireRate, style);
    m = game.add.text(game.world.centerX-950, 0, "raha: " + money, style);
    var b1 = game.add.button(100, 50, 'button1', function() {
      console.log(fireRate);
      if (money >= 50) {
      if (fireRate > 100) {
        fireRate = fireRate - 10;
        t.text = "tulinopeus: " + fireRate;
        money = money - 50;
        m.text = "raha: " + money + " -50";
      }
    }
    if (fireRate < 400) {
      game.stage.backgroundColor = "#fff700";
      bullet.scale.setTo(1.2);
      base.scale.setTo(0.6);
    }
      if (fireRate < 200) {
        game.stage.backgroundColor = "#ff0400";
        bullet.scale.setTo(1.5);
        base.scale.setTo(0.8);
        barrel.scale.setTo(0.8);
        
      }
    });
    b2 = game.add.button(100, 200, 'button2', function() {
      console.log(fireRate);
      if (money >= 700) {
        auto = true;
        money = money - 700;
        m.text = "raha: " + money + " -50";
      }
    });
    
    game.stage.backgroundColor = '#80ff80';
    addChangeStateEventListeners();
    

    var base = game.add.sprite(centerX, centerY, 'base');
    base.anchor.setTo(0.5);
    base.scale.setTo(0.4);

    bullets = game.add.group();
    bullets.enableBody = true;
    bullets.physicsBodyType = Phaser.Physics.ARCADE;
    bullets.createMultiple(50, 'bullet');
    bullets.setAll('checkWorldBounds', true);
    bullets.setAll('outOfBoundsKill', true);
    bullets.setAll('anchor.y', 0.5);
    bullets.setAll('scale.x', 0.85);
    bullets.setAll('scale.y', 0.85);

    barrel = game.add.sprite(centerX, centerY, 'barrel');
    barrel.scale.setTo(0.5);
    barrel.anchor.setTo(0.3, 0.5);
    enemy = game.add.sprite(100, 200, 'adam');
    game.physics.enable(enemy);
    enemy2 = game.add.sprite(100, 200, 'adam');
    game.physics.enable(enemy2);
       
    
      
  },
  update: function(){
    

    if(enemy.x > 0 & enemy.x < 1430 )
    {
    enemy.x = enemy.x + getRandomInt(6);
    
    }
    if(enemy2.x > 0 & enemy2.x < 1430 )
    {
    enemy2.x = enemy2.x + getRandomInt(12);
    
    }
    if(enemy.x > 1420)
    {
      changeState(null, 3);


    }
    if(fireRate == 100)
    {
      if(enemy.x > 0 & enemy.x < 1430 )
      {
      enemy.x = enemy.x + getRandomInt(14);
      
      }
      if(enemy2.x > 0 & enemy2.x < 1430 )
      {
      enemy2.x = enemy2.x + getRandomInt(14);
    
      }
    }
    
    if(auto == false){
      barrel.rotation = game.physics.arcade.angleToPointer(barrel);
        if (game.input.activePointer.isDown) {
          this.fire();
          
        }
    }
    if(auto == true)
    {
      barrel.rotation = game.physics.arcade.angleBetween(barrel,enemy);
      if (game.input.activePointer.isDown) {
        this.fire();
        
      }
    }
    

    game.physics.arcade.overlap(bullets, enemy, this.hitEnemy);
    game.physics.arcade.overlap(bullets, enemy2, this.hitEnemy);
  },
  
  fire: function() {
    if(auto == false)
    {
      if(game.time.now > nextFire) {
        nextFire = game.time.now + fireRate;
        console.log('firing');
        bullet = bullets.getFirstDead();
        bullet.reset(barrel.x, barrel.y);
        
        game.physics.arcade.moveToPointer(bullet, velocity);
        bullet.rotation = game.physics.arcade.angleToPointer(barrel);
      }
    }
    if(auto == true)
    {
      if(game.time.now > nextFire) {
        nextFire = game.time.now + fireRate;
        console.log('firing');
        bullet = bullets.getFirstDead();
        bullet.reset(barrel.x, barrel.y);
  
        game.physics.arcade.moveToObject(bullet,enemy, velocity);
        bullet.rotation = game.physics.arcade.angleToPointer(barrel);
      }
    }
  },
  hitEnemy: function() {
    console.log('hit');
    sound.play('high');
    enemy.kill();
    enemy2.kill();
    
    bullet.kill();
    if (getRandomInt(5) == 2)
    {
        enemy2 = game.add.sprite(100 + getRandomInt(700), 200 + getRandomInt(500), 'adam');
      enemy2.scale.setTo(0.6,0.6);
      game.physics.enable(enemy2);
    }
    enemy = game.add.sprite(100 + getRandomInt(700), 200 + getRandomInt(500), 'adam');
    enemy.scale.setTo(0.3,0.3);
    game.physics.enable(enemy);
    money = money + 10;
    m.text = "raha: " + money;
  },
  
  hitGroup: function(e) {
    bullet.kill();
    e.kill();
  }
};
function getRandomInt(max) {
  return Math.floor(Math.random() * Math.floor(max));
}


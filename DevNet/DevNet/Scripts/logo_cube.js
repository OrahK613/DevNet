﻿var camera, scene, renderer;
//var particle1, particle2, particle4, particle4, particle5, particle6,
//	light1, light2, light3, light4, light5, light6;
var mesh;

var FAR = 300;

var clock = new THREE.Clock();


init();
animate();

function init() {

    renderer = new THREE.WebGLRenderer();
    renderer.setPixelRatio(window.devicePixelRatio);
    renderer.setSize(window.innerWidth, window.innerHeight);
    //document.getElementById("cube").appendChild(renderer.domElement);
    document.body.appendChild(renderer.domElement);



    // CAMERA

    camera = new THREE.PerspectiveCamera(70, window.innerWidth / window.innerHeight, 1, 1000);
    camera.position.z = 400;

    // SCENE

    scene = new THREE.Scene();



    var geometry = new THREE.BoxGeometry(300, 300, 300);

    var texture = THREE.ImageUtils.loadTexture('../Content/textures/large_logo.png');
    texture.anisotropy = renderer.getMaxAnisotropy();

    var material = new THREE.MeshBasicMaterial({ map: texture });
    //var material = new THREE.MeshBasicMaterial({ color: 0xff005a });

    mesh = new THREE.Mesh(geometry, material);
    scene.add(mesh);



    // LIGHTS

    scene.add(new THREE.AmbientLight(0x111111));

    var intensity = 2.5;
    var distance = 100;
    //var c1 = 0xff0040, c2 = 0x0040ff, c3 = 0x80ff80, c4 = 0xffaa00, c5 = 0x00ffaa, c6 = 0xff1100;
    //var c1 = 0xffffff, c2 = 0xffffff, c3 = 0xffffff, c4 = 0xffffff, c5 = 0xffffff, c6 = 0xffffff;

    //var sphere = new THREE.SphereGeometry(2.25, 16, 8);

    //light1 = new THREE.PointLight(c1, intensity, distance);
    //light1.add(new THREE.Mesh(sphere, new THREE.MeshBasicMaterial({ color: c1 })));
    //scene.add(light1);

    //light2 = new THREE.PointLight(c2, intensity, distance);
    //light2.add(new THREE.Mesh(sphere, new THREE.MeshBasicMaterial({ color: c2 })));
    //scene.add(light2);

    //light3 = new THREE.PointLight(c3, intensity, distance);
    //light3.add(new THREE.Mesh(sphere, new THREE.MeshBasicMaterial({ color: c3 })));
    //scene.add(light3);

    //light4 = new THREE.PointLight(c4, intensity, distance);
    //light4.add(new THREE.Mesh(sphere, new THREE.MeshBasicMaterial({ color: c4 })));
    //scene.add(light4);

    //light5 = new THREE.PointLight(c5, intensity, distance);
    //light5.add(new THREE.Mesh(sphere, new THREE.MeshBasicMaterial({ color: c5 })));
    //scene.add(light5);

    //light6 = new THREE.PointLight(c6, intensity, distance);
    //light6.add(new THREE.Mesh(sphere, new THREE.MeshBasicMaterial({ color: c6 })));
    //scene.add(light6);

    var dlight = new THREE.DirectionalLight(0xffffff, 0.1);
    dlight.position.set(0.5, -1, 0).normalize();
    scene.add(dlight);

    // RENDERER


    //renderer.gammaInput = true;
    //renderer.gammaOutput = true;

    //

    window.addEventListener('resize', onWindowResize, false);

}

function onWindowResize() {

    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();

    renderer.setSize(window.innerWidth, window.innerHeight);

}

function animate() {

    requestAnimationFrame(animate);

    mesh.rotation.x += 0.005;
    mesh.rotation.y += 0.01;

    renderer.render(scene, camera);

    //render();

}

function render() {

    var time = Date.now() * 0.00025;
    var z = 20, d = 150;

    light1.position.x = Math.sin(time * 0.7) * d;
    light1.position.z = Math.cos(time * 0.3) * d;

    light2.position.x = Math.cos(time * 0.3) * d;
    light2.position.z = Math.sin(time * 0.7) * d;

    light3.position.x = Math.sin(time * 0.7) * d;
    light3.position.z = Math.sin(time * 0.5) * d;

    light4.position.x = Math.sin(time * 0.3) * d;
    light4.position.z = Math.sin(time * 0.5) * d;

    light5.position.x = Math.cos(time * 0.3) * d;
    light5.position.z = Math.sin(time * 0.5) * d;

    light6.position.x = Math.cos(time * 0.7) * d;
    light6.position.z = Math.cos(time * 0.5) * d;

    //  controls.update(clock.getDelta());

    renderer.render(scene, camera);

}
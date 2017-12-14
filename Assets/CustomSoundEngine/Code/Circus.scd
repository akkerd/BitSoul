(
~filePath = "C:/Users/ANIA/Downloads/Master/4-Sound_Design_for_Digital_Media/Game-SFX/Samples/";
~intro1 = Buffer.read(s, ~filePath ++ "circus_intro1.wav");
~intro2 = Buffer.read(s, ~filePath ++ "circus_intro2.wav");
~intro3 = Buffer.read(s, ~filePath ++ "circus_intro3.wav");
~main = Buffer.read(s, ~filePath ++ "circus_main.wav");

x = SynthDef(\sampler, {
	arg out = 0, rate = 1, amp = 1, buffer = 0;
	var signal;

	signal = PlayBuf.ar(2, buffer, BufRateScale.kr(buffer)*rate, doneAction:2
	);

	Out.ar(out, signal * amp);
});

x.add;


OSCdef.new(
	\start,
	{arg msg;
		[msg].postln;
		if( msg == 1, {x.set(\buffer, ~main); x.play;},{x.stop;});
	},
	'/start',
	nil, 57120
);
)

x.play;
x.set(\buffer, ~main);

s.reboot;
import { type MotionProps, m } from "framer-motion";

import { varContainer } from "./variants";

interface Props extends MotionProps {
	className?: string;
}
/**
 * [whileInView: Elements can be animated when entering and exiting the viewport](https://www.framer.com/motion/scroll-animations/#scroll-triggered-animations)
 *
 * + viewport: [viewport](https://www.framer.com/motion/scroll-animations/###viewport)
 *
 * + once: trigger only once
 */
export default function MotionViewport({ children, className, ...other }: Props) {
	return (
		<m.div
			initial="initial"
			whileInView="animate"
			viewport={{ once: true, amount: 0.3 }}
			variants={varContainer()}
			className={className}
			{...other}
		>
			{children}
		</m.div>
	);
}
